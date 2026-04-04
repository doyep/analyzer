namespace Doyep.Analyzer.Application.Auth;

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
    /// TODO : use case ?
    public static readonly Error InvalidState =
        new("auth.invalid_state", "Invalid state parameter.", 400);

    /// <summary>
    /// Indicates that the scope parameter is invalid.
    /// </summary>
    /// TODO : use case ?
    public static readonly Error InvalidScope =
        new("auth.invalid_scope", "Invalid scope parameter.", 400);

    /// <summary>
    /// Indicates that an error occurred during the token exchange process with Strava.
    /// </summary>
    /// TODO : use case ?
    public static readonly Error StravaError =
        new("auth.strava_error", "Error occurred during token exchange with Strava.", 500);

    /// <summary>
    /// Indicates that there was a failure when trying to save the Strava token to the database, which could be due to a database error or an issue with the token data itself.
    /// </summary>
    public static readonly Error FailedToSaveStravaToken =
        new("auth.failed_to_save_strava_token", "Failed to save Strava token to the database.", 500);

    /// <summary>
    /// Indicates that the authenticated Strava athlete is not authorized to access the application,
    /// likely because their Strava ID is not present in the application's whitelist of allowed users.
    /// </summary>
    /// TODO : use case ?
    public static readonly Error UnauthorizedAthlete =
        new("auth.unauthorized_athlete", "The authenticated Strava athlete is not authorized to access the application.", 403);
}
