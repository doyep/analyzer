namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents a summary of athlete informations
/// </summary>
public record SummaryAthlete(
    long Id,
    int ResourceState,
    string Firstname,
    string Lastname,
    string? ProfileMedium,
    string? Profile,
    string? City,
    string? State,
    string? Country,
    string Sex,
    bool Premium,
    bool Summit,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
