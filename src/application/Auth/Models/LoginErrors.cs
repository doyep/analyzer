namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines a set of standardized error instances related to the login process,
/// providing consistent error codes, messages, and HTTP status codes for common login failures.
/// </summary>
public static class LoginErrors
{
    /// <summary>
    /// Indicates that access was denied during the Strava authentication process,
    /// which can occur if the user cancels the authentication flow.
    /// </summary>
    public static readonly Error AccessDenied =
        new("login.access_denied", "Access denied during the Strava authentication process.", 403);

    /// <summary>
    /// Indicates that the state parameter is missing or does not match the expected value.
    /// </summary>
    public static readonly Error InvalidState =
        new("login.invalid_state", "Invalid state parameter.", 400);

    /// <summary>
    /// Indicates that the scope parameter does not match the required values.
    /// </summary>
    public static readonly Error InvalidScope =
        new("login.invalid_scope", "Invalid scope parameter.", 400);

    /// <summary>
    /// Indicates that the authorization code parameter is missing.
    /// </summary>
    public static readonly Error MissingAuthorizationCode =
        new("login.missing_authorization_code", "Missing authorization code parameter.", 400);
}
