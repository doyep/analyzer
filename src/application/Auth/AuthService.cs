using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Provides services for managing user authentication, including login and logout operations, by integrating with Strava for athlete data and access control.
/// </summary>
public class AuthService(
    IAthleteService _athleteService,
    IJwtTokenService _jwtTokenService,
    IRefreshTokenService _refreshTokenService,
    IStravaAuthenticationService _stravaAuthenticationService,
    IStravaTokenRepository _stravaTokenRepository
) : IAuthService
{
    /// <inheritdoc/>
    public async Task<Result<AuthTokens, Error>> LoginAsync(string authorizationCode)
    {
        var stravaTokenResponse = await _stravaAuthenticationService.ExchangeToken(authorizationCode);

        var result = await _athleteService.GetAuthorizedAthleteAsync(stravaTokenResponse.Athlete);
        if (result.IsFailure)
        {
            await _stravaAuthenticationService.Deauthorize(stravaTokenResponse.AccessToken);
            return Result<AuthTokens, Error>.Failure(result.Error);
        }

        var stravaToken = StravaToken.CreateFrom(stravaTokenResponse);
        try
        {
            await _stravaTokenRepository.SaveAsync(stravaToken);
        }
        catch (StravaTokenPersistenceException)
        {
            return Result<AuthTokens, Error>.Failure(AuthErrors.FailedToSaveStravaToken);
        }

        var jwtToken = _jwtTokenService.Generate(result.Value);
        var refreshToken = await _refreshTokenService.IssueRefreshTokenAsync(result.Value.StravaAthleteId);

        return Result<AuthTokens, Error>.Success(new AuthTokens
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken
        });
    }

    /// <inheritdoc/>
    public Task LogoutAsync(long stravaAthleteId)
    {
        throw new NotImplementedException();
    }
}
