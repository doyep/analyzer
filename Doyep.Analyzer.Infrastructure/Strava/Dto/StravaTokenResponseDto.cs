using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
public record StravaTokenResponseDto
{
    /// <summary>
    /// Always <c>Bearer</c>
    /// </summary>
    [property: JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    /// <summary>
    /// Seconds until the short-lived access token will expire
    /// </summary>
    [property: JsonPropertyName("expires_in")]
    public required int ExpiresIn { get; init; }

    /// <summary>
    /// The number of seconds since the epoch when the provided access token will expire
    /// </summary>
    [property: JsonPropertyName("expires_at")]
    public required long ExpiresAt { get; init; }

    /// <summary>
    /// The refresh token for this user, to be used to get the next access token for this user. Please expect that this value can change anytime you retrieve a new access token. Once a new refresh token code has been returned, the older code will no longer work.
    /// </summary>
    [property: JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; init; }

    /// <summary>
    /// The access token, to be used to fetch personnal data for this user.
    /// </summary>
    [property: JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    /// <summary>
    /// The auth <see cref="SummaryAthleteDto"/>.
    /// </summary>
    [property: JsonPropertyName("athlete")]
    public required SummaryAthleteDto? Athlete { get; init; }
}
