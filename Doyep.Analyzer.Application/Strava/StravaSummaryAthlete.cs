namespace Doyep.Analyzer.Application;

/// <summary>
/// Represents Athlete informations
/// </summary>
public record StravaSummaryAthlete(
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