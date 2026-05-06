namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines constant values for cookie names used in the authentication process.
/// </summary>
public static class AuthCookieConstants
{
    /// <summary>
    /// The name of the cookie used to store the access token after successful authentication.
    /// </summary>
    public const string AccessToken = "access_token";

    /// <summary>
    /// The name of the cookie used to store the refresh token for renewing access tokens without requiring re-authentication.
    /// </summary>
    public const string RefreshToken = "refresh_token";

    /// <summary>
    /// The name of the cookie used to store the state parameter during the Strava OAuth authentication flow,
    /// helping to prevent CSRF attacks and maintain state between the login request and callback.
    /// </summary>
    public const string StravaAuthState = "strava_auth_state";
}
