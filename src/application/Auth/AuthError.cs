namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Enumeration representing possible authentication errors during the Strava OAuth callback process.
/// </summary>
public enum AuthError
{
    /// <summary>
    /// Indicates that the user denied access during the authentication process.
    /// </summary>
    AccessDenied,

    /// <summary>
    /// Indicates that there was an issue with the authentication request, such as missing parameters.
    /// </summary>
    InvalidRequest,

    /// <summary>
    /// Indicates that the state parameter is missing or does not match the expected value.
    /// </summary>
    InvalidState,

    /// <summary>
    /// Indicates that the provided scope does not include all required permissions to access the application.
    /// </summary>
    InvalidScope,

    /// <summary>
    /// Indicates that there was an error during the token exchange process with Strava, such as an invalid authorization code or network issues.
    /// </summary>
    StravaError,

    /// <summary>
    /// Indicates that the authenticated Strava athlete is not authorized to access the application, likely because their Strava ID is not present in the application's whitelist of allowed users.
    /// </summary>
    Unauthorized
}
