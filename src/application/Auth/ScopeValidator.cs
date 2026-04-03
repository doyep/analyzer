namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Utility class to validate if the scopes granted by the user include all the required scopes for the application to works properly.
/// </summary>
public static class ScopeValidator
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
        var scopes = scopesRaw.Split(',');
        return RequiredScopes.All(scopes.Contains);
    }
}
