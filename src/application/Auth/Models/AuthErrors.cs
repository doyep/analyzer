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

    /// <summary>
    /// Indicates that there was a failure when trying to refresh the authentication tokens (JWT and refresh token), which could be due to an unexpected error in the token generation logic or an issue with the underlying services.
    /// </summary>
    public static readonly Error FailedToRefreshToken =
        new("auth.failed_to_refresh_token", "Failed to refresh authentication tokens.", 401);
}
