namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Represents a Strava access token for a specific athlete.
/// </summary>
public class EncryptedStravaToken
{
    /// <summary>
    /// The Strava athlete ID associated with this token.
    /// </summary>
    public long StravaAthleteId { get; init; }

    /// <summary>
    /// The access token string used for authenticating API requests to Strava on behalf of the athlete.
    /// This value is stored in an encrypted form in the database.
    /// </summary>
    public string EncryptedAccessToken { get; set; } = default!;

    /// <summary>
    /// The refresh token string used for obtaining new access tokens without requiring the user to re-authenticate.
    /// This value is stored in an encrypted form in the database.
    /// </summary>
    public string EncryptedRefreshToken { get; set; } = default!;

    /// <summary>
    /// The date and time when the access token expires.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; }
}
