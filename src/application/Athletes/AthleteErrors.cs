namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Defines a set of standardized error instances related to athlete operations,
/// providing consistent error codes, messages, and HTTP status codes for common failure
/// scenarios such as duplicate athletes and athletes not found in the repository.
/// </summary>
public static class AthleteErrors
{
    /// <summary>
    /// Indicates that no athlete was found with the specified Strava ID, which can occur when trying to access or manipulate an athlete that does not exist in the repository.
    /// </summary>
    public static readonly Error NotFoundAthlete =
        new("athlete.not_found", "No athlete found with the specified Strava ID.", 404);

    /// <summary>
    /// Indicates that the authenticated Strava athlete is not authorized to access the application,
    /// likely because their Strava ID is not present in the application's whitelist of allowed users.
    /// </summary>
    public static readonly Error UnauthorizedAthlete =
        new("athlete.unauthorized", "The authenticated Strava athlete is not authorized to access the application.", 403);
}
