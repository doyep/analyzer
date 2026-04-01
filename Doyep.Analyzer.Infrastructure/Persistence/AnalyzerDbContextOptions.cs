namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Configuration options for the AnalyzerDbContext.
/// These values are loaded from the "ConnectionStrings" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class AnalyzerDbContextOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "ConnectionStrings";

    /// <summary>
    /// The default connection string used to connect to the database.
    /// </summary>
    public required string DefaultConnection { get; set; }

}
