namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents Athlete informations
/// </summary>
public record Athlete(
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
