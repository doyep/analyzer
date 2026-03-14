namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
public record StravaTokenResponse(
    string TokenType,
    TimeSpan ExpiresIn,
    DateTimeOffset ExpiresAt,
    string RefreshToken,
    string AccessToken,
    SummaryAthlete? Athlete
);
