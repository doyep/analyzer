namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines a set of standardized error instances related to authentication operations,
/// providing consistent error codes, messages, and HTTP status codes for common authentication
/// failure scenarios such as access denial, invalid parameters, Strava integration issues,
/// and athlete authorization problems.
/// </summary>
public static class AuthErrors
{
    /// <summary>
    /// Indicates that access was denied during the authentication process, which can occur if the user cancels the authentication flow.
    /// </summary>
    public static readonly Error AccessDenied =
        new("auth.access_denied", "Access denied during the authentication process.", 403);

    /// <summary>
    /// Indicates that the state parameter is missing or does not match the expected value.
    /// </summary>
    public static readonly Error InvalidState =
        new("auth.invalid_state", "Invalid state parameter.", 400);

    /// <summary>
    /// Indicates that the scope parameter is invalid.
    /// </summary>
    public static readonly Error InvalidScope =
        new("auth.invalid_scope", "Invalid scope parameter.", 400);

    /// <summary>
    /// Indicates that an error occurred during the token exchange process with Strava.
    /// </summary>
    public static readonly Error StravaError =
        new("auth.strava_error", "Error occurred during token exchange with Strava.", 500);

    /// <summary>
    /// Indicates that there was a failure when trying to save the Strava token to the database, which could be due to a database error or an issue with the token data itself.
    /// </summary>
    public static readonly Error FailedToSaveStravaToken =
        new("auth.failed_to_save_strava_token", "Failed to save Strava token to the database.", 500);

    /// <summary>
    /// Indicates that there was a failure when trying to generate authentication tokens (JWT and refresh token), which could be due to an unexpected error in the token generation logic or an issue with the underlying services.
    /// </summary>
    public static readonly Error FailedToGenerateTokens =
        new("auth.failed_to_generate_tokens", "Failed to generate authentication tokens.", 500);
}
