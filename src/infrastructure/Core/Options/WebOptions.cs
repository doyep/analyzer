namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Configuration options for the Analyzer frontend.
/// These values are loaded from the "Web" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class WebOptions
{
    /// <summary>
    /// Section name of appsettings.json.
    /// </summary>
    public const string SectionName = "Web";

    /// <summary>
    /// Base URL of the application, used for building redirect URIs and links.
    /// </summary>
    public required string BaseUrl { get; set; }
}
