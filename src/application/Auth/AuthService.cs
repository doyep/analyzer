using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Provides services for managing user authentication, including login and logout operations, by integrating with Strava for athlete data and access control.
/// </summary>
public class AuthService(
    IAthleteService _athleteService,
    IJwtTokenService _jwtTokenService,
    IRefreshTokenService _refreshTokenService,
    IStateService _stateService,
    IStravaAuthenticationService _stravaAuthenticationService,
    IStravaTokenRepository _stravaTokenRepository
) : IAuthService
{
    /// <inheritdoc/>
    public async Task<Result<AuthTokens, Error>> LoginAsync(string authorizationCode, string state)
    {
        var statePayloadResult = ConsumeState(state);
        if (statePayloadResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(statePayloadResult.Error);

        var stravaTokenResponseResult = await ExchangeTokenAsync(authorizationCode);
        if (stravaTokenResponseResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(stravaTokenResponseResult.Error);

        var athleteResult = await GetAuthorizedAthleteAsync(stravaTokenResponseResult.Value);
        if (athleteResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(athleteResult.Error);

        var saveResult = await SaveStravaTokenAsync(stravaTokenResponseResult.Value);
        if (saveResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(saveResult.Error);

        return await GenerateTokensAsync(athleteResult.Value, statePayloadResult.Value);
    }

    private Result<StatePayload, Error> ConsumeState(string state)
    {
        var statePayload = _stateService.Consume(state);
        if (statePayload is null)
            return Result<StatePayload, Error>.Failure(AuthErrors.InvalidState);

        return Result<StatePayload, Error>.Success(statePayload);
    }

    private async Task<Result<StravaAuthTokenResponse, Error>> ExchangeTokenAsync(string authorizationCode)
    {
        try
        {
            var stravaTokenResponse = await _stravaAuthenticationService.ExchangeTokenAsync(authorizationCode);
            return Result<StravaAuthTokenResponse, Error>.Success(stravaTokenResponse);
        }
        catch (Exception)
        {
            return Result<StravaAuthTokenResponse, Error>.Failure(AuthErrors.StravaError);
        }
    }

    private async Task<Result<Athlete, Error>> GetAuthorizedAthleteAsync(StravaAuthTokenResponse stravaTokenResponse)
    {
        var athleteResult = await _athleteService.GetAuthorizedAthleteAsync(stravaTokenResponse.Athlete);
        if (athleteResult.IsFailure)
        {
            await _stravaAuthenticationService.Deauthorize(stravaTokenResponse.AccessToken);
            return Result<Athlete, Error>.Failure(athleteResult.Error);
        }

        return Result<Athlete, Error>.Success(athleteResult.Value);
    }

    private async Task<Result<Task, Error>> SaveStravaTokenAsync(StravaAuthTokenResponse stravaTokenResponse)
    {
        try
        {
            var stravaToken = StravaToken.CreateFrom(stravaTokenResponse);
            await _stravaTokenRepository.SaveAsync(stravaToken);

            return Result<Task, Error>.Success(Task.CompletedTask);
        }
        catch (StravaTokenPersistenceException)
        {
            return Result<Task, Error>.Failure(AuthErrors.FailedToSaveStravaToken);
        }
    }

    private async Task<Result<AuthTokens, Error>> GenerateTokensAsync(Athlete athlete, StatePayload statePayload)
    {
        try
        {
            var jwtToken = _jwtTokenService.Generate(athlete);
            var refreshToken = await _refreshTokenService.IssueRefreshTokenAsync(athlete.StravaAthleteId, statePayload.DeviceId);

            return Result<AuthTokens, Error>.Success(new AuthTokens
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            });
        }
        catch (RefreshTokenPersistenceException)
        {
            return Result<AuthTokens, Error>.Failure(AuthErrors.FailedToGenerateTokens);
        }
    }

    /// <inheritdoc/>
    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshTokenService.TryRevokeAsync(refreshToken);
    }
}
