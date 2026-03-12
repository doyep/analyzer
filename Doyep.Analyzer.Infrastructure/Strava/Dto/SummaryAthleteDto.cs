using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents a Strava Athlete with summary informations
/// </summary>
public record SummaryAthleteDto(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("resource_state")] int ResourceState,
    [property: JsonPropertyName("firstname")] string Firstname,
    [property: JsonPropertyName("lastname")] string Lastname,
    [property: JsonPropertyName("profile_medium")] string? ProfileMedium,
    [property: JsonPropertyName("profile")] string? Profile,
    [property: JsonPropertyName("city")] string? City,
    [property: JsonPropertyName("state")] string? State,
    [property: JsonPropertyName("country")] string? Country,
    [property: JsonPropertyName("sex")] string Sex,
    [property: JsonPropertyName("premium")] bool Premium,
    [property: JsonPropertyName("summit")] bool Summit,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("updated_at")] DateTime UpdatedAt
);
