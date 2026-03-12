using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
public record StravaTokenResponseDto(
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("expires_at")] long ExpiresAt,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("athlete")] SummaryAthleteDto? Athlete
);
