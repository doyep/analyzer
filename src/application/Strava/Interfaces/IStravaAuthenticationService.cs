namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Defines the contract for handling authentication and token management with the Strava API.
/// </summary>
public interface IStravaAuthenticationService
{
    /// <summary>
    /// Redirects the user to the Strava OAuth authorization URL to initiate the login workflow.
    /// </summary>
    /// <param name="deviceId">The unique identifier for the user's device.</param>
    /// <returns>The formatted URL string for the Strava login page.</returns>
    string GenerateLoginUrl(Guid deviceId);

    /// <summary>
    /// Exchange an authorization code with a full token response and summary authenticated Athlete.
    /// </summary>
    /// <param name="authorizationCode">The code received from the Strava OAuth callback.</param>
    /// <returns>A task representing the token response and the summary authenticated Athlete or null if exchange fails.</returns>
    /// <exception cref="StravaAuthenticationException">Thrown when the token exchange process fails due to invalid credentials, network issues, or unexpected API responses.</exception>
    Task<StravaAuthTokenResponse> ExchangeTokenAsync(string authorizationCode);

    /// <summary>
    /// Refreshes an expired access token with a refresh token.
    /// </summary>
    /// <param name="refreshToken">A valid refresh token.</param>
    /// <returns>A task representing the new token response or null if renewal fails.</returns>
    /// <exception cref="StravaAuthenticationException">Thrown when the token refresh process fails due to invalid credentials, network issues, or unexpected API responses.</exception>
    Task<StravaRefreshTokenResponse> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Revoke the current access token from the application.
    /// </summary>
    /// <param name="accessToken">An active access token to be invalidated.</param>
    /// <returns>A task representing the asynchronous deauthorization process.</returns>
    Task Deauthorize(string accessToken);
}
