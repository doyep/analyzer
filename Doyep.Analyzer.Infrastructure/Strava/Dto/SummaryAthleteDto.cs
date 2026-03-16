using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents a summary of Strava Athlete informations.
/// </summary>
public record SummaryAthleteDto
{
    /// <summary>
    /// The unique identifier of the athlete
    /// </summary>
    [property: JsonPropertyName("id")]
    public required long Id { get; init; }

    /// <summary>
    /// Resource state, indicates level of detail. Possible values: 1 -> "meta", 2 -> "summary", 3 -> "detail"
    /// </summary>
    [property: JsonPropertyName("resource_state")]
    public required int ResourceState { get; init; }

    /// <summary>
    /// The athlete's first name.
    /// </summary>
    [property: JsonPropertyName("firstname")]
    public required string Firstname { get; init; }

    /// <summary>
    /// The athlete's last name.
    /// </summary>
    [property: JsonPropertyName("lastname")]
    public required string Lastname { get; init; }

    /// <summary>
    /// URL to a 62x62 pixel profile picture.
    /// </summary>
    [property: JsonPropertyName("profile_medium")]
    public required string? ProfileMedium { get; init; }

    /// <summary>
    /// URL to a 124x124 pixel profile picture.
    /// </summary>
    [property: JsonPropertyName("profile")]
    public required string? Profile { get; init; }

    /// <summary>
    /// The athlete's city.
    /// </summary>
    [property: JsonPropertyName("city")]
    public required string? City { get; init; }

    /// <summary>
    /// The athlete's state or geographical region.
    /// </summary>
    [property: JsonPropertyName("state")]
    public required string? State { get; init; }

    /// <summary>
    /// The athlete's country.
    /// </summary>
    [property: JsonPropertyName("country")]
    public required string? Country { get; init; }

    /// <summary>
    /// The athlete's sex. May take one of the following values: M, F
    /// </summary>
    [property: JsonPropertyName("sex")]
    public required string Sex { get; init; }

    /// <summary>
    /// Deprecated. Use summit field instead. Whether the athlete has any Summit subscription.
    /// </summary>
    [property: JsonPropertyName("premium")]
    public required bool Premium { get; init; }

    /// <summary>
    /// Whether the athlete has any Summit subscription.
    /// </summary>
    [property: JsonPropertyName("summit")]
    public required bool Summit { get; init; }

    /// <summary>
    /// The time at which the athlete was created.
    /// </summary>
    [property: JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The time at which the athlete was last updated.
    /// </summary>
    [property: JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
