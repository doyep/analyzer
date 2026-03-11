namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Configuration options for the Strava API integration.
/// These values are loaded from the "Strava" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class StravaOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "Strava";

    /// <summary>
    /// Base URL of the Strava API (https://www.strava.com)
    /// </summary>
    public required string BaseUrl { get; set; }

    /// <summary>
    /// OAuth 2.0 client identifier provided by Strava for the application
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// OAuth 2.0 client secret associated with the application.
    /// This value should be stored securely.
    /// </summary>
    public required string ClientSecret { get; set; }
}
