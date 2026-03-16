namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Provides authentication and token management throught the Strava Api
/// </summary>
public interface IStravaAuthenticationService
{
    /// <summary>
    /// Redirects the user to the Strava OAuth authorization URL to initiate the login workflow.
    /// </summary>
    /// <returns>The formatted URL string for the Strava login page.</returns>
    string GenerateLoginUrl();

    /// <summary>
    /// Exchange an authorization code with a full token response and summary authenticated Athlete.
    /// </summary>
    /// <param name="authorizationCode">The code recieved from the Strava OAuth callback.</param>
    /// <returns>A task representing the token response and the summary authenticated Athlete or null if exchange fails.</returns>
    Task<StravaTokenResponse?> ExchangeToken(string authorizationCode);
    /// <summary>
    /// Refreshes an expired access token with a refresh token.
    /// </summary>
    /// <param name="refreshToken">A valid refresh token.</param>
    /// <returns>A task representing the new token response or null if renewall fails.</returns>
    Task<StravaTokenResponse?> RefreshToken(string refreshToken);

    /// <summary>
    /// Revoke the current access token from the application.
    /// </summary>
    /// <param name="accessToken">An active access token to be invalidated.</param>
    /// <returns>A task representing the asynchronous deauthorization process.</returns>
    Task Deauthorize(string accessToken);
}
