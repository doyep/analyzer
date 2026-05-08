namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Utility class to validate if the scopes granted by the user include all the required scopes for the application to works properly.
/// </summary>
public static class StravaAuthorizationScopes
{
    /// <summary>
    /// The list of scopes required by the application to works properly.
    /// These scopes are required to be included in the authorization URL and to be granted by the user during the OAuth flow.
    /// </summary>
    public static readonly string[] RequiredScopes = [
        "read",
        "read_all",
        "profile:read_all",
        "activity:read_all"
    ];

    /// <summary>
    /// Checks if the scopes granted by the user include all the required scope for the application to works properly.
    /// </summary>
    /// <param name="scopesRaw">The list of scopes granted by the user</param>
    /// <returns>True if all the required scopes are included in the granted scopes, false otherwise.</returns>
    public static bool HasRequiredScope(string scopesRaw)
    {
        var scopes = scopesRaw
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();

        return RequiredScopes.All(required =>
            scopes.Any(scope => string.Equals(scope, required, StringComparison.OrdinalIgnoreCase))
        );
    }

    /// <summary>
    /// Joins the required scopes into a single string separated by commas, to be used in the authorization URL.
    /// </summary>
    /// <returns>A string containing all the required scopes separated by commas.</returns>
    public static string JoinRequiredScopes() => string.Join(",", RequiredScopes);
}
