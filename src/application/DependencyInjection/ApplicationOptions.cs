namespace Doyep.Analyzer.Application;

/// <summary>
/// Configuration options for the Analyzer application.
/// These values are loaded from the "Application" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class ApplicationOptions
{
    /// <summary>
    /// Section name of appsettings.json.
    /// </summary>
    public const string SectionName = "Application";

    /// <summary>
    /// Base URL of the application, used for building redirect URIs and links.
    /// </summary>
    public required string BaseUrl { get; set; }
}
