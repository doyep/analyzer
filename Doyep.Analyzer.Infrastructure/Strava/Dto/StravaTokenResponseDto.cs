using System.Text.Json.Serialization;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
/// <param name="TokenType">Always <c>Bearer</c></param>
/// <param name="ExpiresIn">Seconds until the short-lived access token will expire</param>
/// <param name="ExpiresAt">The number of seconds since the epoch when the provided access token will expire</param>
/// <param name="RefreshToken">The refresh token for this user, to be used to get the next access token for this user. Please expect that this value can change anytime you retrieve a new access token. Once a new refresh token code has been returned, the older code will no longer work.</param>
/// <param name="AccessToken">The access token, to be used to fetch personnal data for this user.</param>
/// <param name="Athlete">The auth <see cref="SummaryAthleteDto"/>.</param>
public record StravaTokenResponseDto(
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("expires_at")] long ExpiresAt,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("athlete")] SummaryAthleteDto? Athlete
);
