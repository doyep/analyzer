namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint.
/// </summary>
public abstract record StravaTokenResponse
{
    public required string TokenType { get; init; }
    public required TimeSpan ExpiresIn { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required string RefreshToken { get; init; }
    public required string AccessToken { get; init; }
}

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint when exchanging an authorization code for an access token (which includes athlete information).
/// </summary>
public record StravaAuthTokenResponse : StravaTokenResponse
{
    public required StravaSummaryAthlete Athlete { get; init; }
}

/// <summary>
/// Represents the response returned by the Strava token exchange endpoint when refreshing an access token (which does not include athlete information).
/// </summary>
public record StravaRefreshTokenResponse : StravaTokenResponse
{
}
