using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Implements the login service responsible for handling user authentication with Strava.
/// This service provides methods to generate the Strava login URL and to process the login workflow by
/// exchanging the authorization code for authentication tokens.
/// </summary>
public class LoginService(
    IAthleteService athleteService,
    IJwtTokenService jwtTokenService,
    IRefreshTokenService refreshTokenService,
    IStravaAuthService stravaAuthService,
    IStravaTokenRepository stravaTokenRepository
) : ILoginService
{
    /// <inheritdoc/>
    public Uri GenerateStravaLoginUrl(string state, Uri callbackUri)
    {
        return stravaAuthService.GenerateLoginUrl(state, callbackUri);
    }

    /// <inheritdoc/>
    public async Task<Result<AuthTokens, Error>> LoginWithStravaAsync(string authorizationCode, Guid deviceId)
    {
        var tokenResult = await stravaAuthService.ExchangeTokenAsync(authorizationCode);
        if (tokenResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(tokenResult.Error);

        var athleteResult = await FindAuthorizedAthleteOrDeauthorize(tokenResult.Value);
        if (athleteResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(athleteResult.Error);

        var saveResult = await SaveStravaTokenAsync(tokenResult.Value);
        if (saveResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(saveResult.Error);

        return await GenerateTokensAsync(athleteResult.Value, deviceId);
    }

    /// <summary>
    /// Finds an authorized athlete based on the Strava authentication token response.
    /// If the athlete is not authorized, it deauthorizes the Strava token to prevent unauthorized access.
    /// </summary>
    /// <param name="stravaTokenResponse">The Strava authentication token response containing the athlete information.</param>
    /// <returns>A result containing the authorized athlete or an error if the athlete is not authorized.</returns>
    private async Task<Result<Athlete, Error>> FindAuthorizedAthleteOrDeauthorize(StravaAuthTokenResponse stravaTokenResponse)
    {
        var athlete = await athleteService.EnsureAthleteAsync(stravaTokenResponse.Athlete);
        if (!athlete.HasAccess())
        {
            await stravaAuthService.Deauthorize(stravaTokenResponse.AccessToken);

            return Result<Athlete, Error>.Failure(AthleteErrors.UnauthorizedAthlete);
        }

        return Result<Athlete, Error>.Success(athlete);
    }

    /// <summary>
    /// Saves the Strava authentication token in the repository. If saving fails, it returns an error result.
    /// </summary>
    /// <param name="stravaTokenResponse">The Strava authentication token response containing the tokens to be saved.</param>
    /// <returns>A result indicating success or failure of the save operation.</returns>
    private async Task<Result<Error>> SaveStravaTokenAsync(StravaAuthTokenResponse stravaTokenResponse)
    {
        try
        {
            var stravaToken = StravaToken.CreateFrom(stravaTokenResponse);
            await stravaTokenRepository.SaveAsync(stravaToken);

            return Result<Error>.Success();
        }
        catch (StravaTokenPersistenceException)
        {
            return Result<Error>.Failure(AuthErrors.FailedToSaveStravaToken);
        }
    }

    /// <summary>
    /// Generates authentication tokens (JWT and refresh token) for the authenticated athlete. If token generation fails, it returns an error result.
    /// </summary>
    /// <param name="athlete">The authenticated athlete for whom the tokens are being generated.</param>
    /// <param name="deviceId">The device ID associated with the authentication request.</param>
    /// <returns>A result containing the generated authentication tokens or an error if token generation fails.</returns>
    private async Task<Result<AuthTokens, Error>> GenerateTokensAsync(Athlete athlete, Guid deviceId)
    {
        var jwt = jwtTokenService.Generate(athlete);

        var refreshTokenResult = await refreshTokenService.IssueRefreshTokenAsync(athlete.StravaAthleteId, deviceId);
        if (refreshTokenResult.IsFailure)
        {
            return Result<AuthTokens, Error>.Failure(refreshTokenResult.Error);
        }

        return Result<AuthTokens, Error>.Success(new AuthTokens
        {
            JwtToken = jwt,
            RefreshToken = refreshTokenResult.Value
        });
    }
}
