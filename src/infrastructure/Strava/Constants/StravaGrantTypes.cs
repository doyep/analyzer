namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Grant types used in the Strava Oauth 2.0 workflow.
/// </summary>
public static class StravaGrantTypes
{
    public const string AuthorizationCode = "authorization_code";
    public const string RefreshToken = "refresh_token";
}