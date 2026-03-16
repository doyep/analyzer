namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Static class containing constants related to the Strava API integration, such as base URLs and endpoint paths.
/// </summary>
public static class StravaEndpoints
{
    /// <summary>
    /// Base URL of the Strava
    /// </summary>
    public const string BaseUrl = "https://www.strava.com";

    /// <summary>
    /// Endpoint for initiating the OAuth 2.0 authorization flow with Strava.
    /// </summary>
    public const string AuthorizeEndpoint = "oauth/authorize";

    /// <summary>
    /// Endpoint for exchanging an authorization code for an access token with Strava.
    /// </summary>
    public const string TokenEndpoint = "oauth/token";

    /// <summary>
    /// Endpoint for deauthorizing the application from a user's Strava account.
    /// </summary>
    public const string DeauthorizeEndpoint = "oauth/deauthorize";
}
