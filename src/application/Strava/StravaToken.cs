namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents a Strava access token for a specific athlete.
/// </summary>
public class StravaToken
{
    /// <summary>
    /// The Strava athlete ID associated with this token.
    /// </summary>
    public long StravaAthleteId { get; init; }

    /// <summary>
    /// The access token string used for authenticating API requests to Strava on behalf of the athlete.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// The refresh token string used for obtaining new ccess tokens without requiring the user to re-authenticate.
    /// </summary>
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    /// The date and time when the access token expires.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; }

    /// <summary>
    /// Indicates whether the access token is currently active (not expired).
    /// </summary>
    public bool IsActive => DateTimeOffset.UtcNow < ExpiresAt;

    /// <summary>
    /// Updates the current StravaToken with new values from another StravaToken instance.
    /// </summary>
    public void UpdateFrom(StravaToken stravaToken)
    {
        AccessToken = stravaToken.AccessToken;
        RefreshToken = stravaToken.RefreshToken;
        ExpiresAt = stravaToken.ExpiresAt;
    }

    public static StravaToken CreateFrom(StravaTokenResponse tokenResponse)
    {
        return new StravaToken
        {
            /// TODO : Improve type safety
            StravaAthleteId = tokenResponse.Athlete?.Id ?? throw new ArgumentException("Token response must include athlete information."),
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = tokenResponse.ExpiresAt
        };
    }
}
