namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Provides services for managing user authentication, including login and logout operations, by integrating with Strava for athlete data and access control.
/// </summary>
public class AuthService(
    IJwtTokenService _jwtTokenService,
    IRefreshTokenService _refreshTokenService
) : IAuthService
{
    /// <inheritdoc/>
    public async Task<Result<AuthTokens, Error>> RefreshTokenAsync(string refreshToken)
    {
        var refreshTokenResult = await _refreshTokenService.RefreshAsync(refreshToken);
        if (refreshTokenResult.IsFailure)
            return Result<AuthTokens, Error>.Failure(refreshTokenResult.Error);

        var jwtToken = _jwtTokenService.Generate(refreshTokenResult.Value.Athlete);
        return Result<AuthTokens, Error>.Success(new AuthTokens
        {
            JwtToken = jwtToken,
            RefreshToken = refreshTokenResult.Value.RefreshToken
        });
    }

    /// <inheritdoc/>
    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshTokenService.RevokeAsync(refreshToken);
    }
}
