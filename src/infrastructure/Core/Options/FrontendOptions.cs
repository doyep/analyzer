namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Configuration options for the Analyzer frontend.
/// These values are loaded from the "Frontend" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class FrontendOptions
{
    /// <summary>
    /// Section name of appsettings.json.
    /// </summary>
    public const string SectionName = "Frontend";

    /// <summary>
    /// Base URL of the application, used for building redirect URIs and links.
    /// </summary>
    public required string BaseUrl { get; set; }
}
