namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
public record StravaTokenResponse(
    string TokenType,
    int ExpiresIn,
    long ExpiresAt,
    string RefreshToken,
    string AccessToken,
    SummaryAthlete Athlete
);

/// <summary>
/// Represent the response returned by the Strava token refresh endpoint
/// </summary>
public record StravaRefreshTokenResponse(
    string TokenType,
    int ExpiresIn,
    long ExpiresAt,
    string RefreshToken,
    string AccessToken
);
