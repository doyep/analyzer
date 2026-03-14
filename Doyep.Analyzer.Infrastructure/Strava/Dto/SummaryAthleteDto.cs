using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents a summary of Strava Athlete informations.
/// </summary>
/// <param name="Id">The unique identifier of the athlete</param>
/// <param name="ResourceState">Resource state, indicates level of detail. Possible values: 1 -> "meta", 2 -> "summary", 3 -> "detail"</param>
/// <param name="Firstname">The athlete's first name.</param>
/// <param name="Lastname">The athlete's last name.</param>
/// <param name="ProfileMedium">URL to a 62x62 pixel profile picture.</param>
/// <param name="Profile">URL to a 124x124 pixel profile picture.</param>
/// <param name="City">The athlete's city.</param>
/// <param name="State">The athlete's state or geographical region.</param>
/// <param name="Country">The athlete's country.</param>
/// <param name="Sex">The athlete's sex. May take one of the following values: M, F</param>
/// <param name="Premium">Deprecated. Use summit field instead. Whether the athlete has any Summit subscription.</param>
/// <param name="Summit">Whether the athlete has any Summit subscription.</param>
/// <param name="CreatedAt">The time at which the athlete was created.</param>
/// <param name="UpdatedAt">The time at which the athlete was last updated.</param>
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
