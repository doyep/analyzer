namespace Doyep.Analyzer.Domain;

/// <summary>
/// Represents a refresh token used for obtaining new access tokens without requiring the user to re-authenticate.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// The unique identifier for the refresh token. This is used to track and manage refresh tokens in the database. It is not exposed to the client and is not used in the token exchange process.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The Strava athlete ID associated with this refresh token.
    /// </summary>
    public long StravaAthleteId { get; init; }

    /// <summary>
    /// The hashed value of the refresh token.
    /// </summary>
    public string HashedToken { get; private set; } = default!;

    /// <summary>
    /// The date and time when the refresh token was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// The date and time when the refresh token expires.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; private set; }

    /// <summary>
    /// The date and time when the refresh token was revoked. If null, the token is still active.
    /// </summary>
    public DateTimeOffset? RevokedAt { get; private set; }

    /// <summary>
    /// Indicate whether the refresh token is currently active (not revoked and not expired).
    /// </summary>
    public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow < ExpiresAt;

    /// <summary>
    /// Revokes the refresh token by setting the revocation timestamp.
    /// Once revoked, the token is no longer active.
    /// </summary>
    public void Revoke()
    {
        RevokedAt = DateTimeOffset.UtcNow;
    }

    public static RefreshToken Create(long stravaAthleteId, string hashedToken, DateTimeOffset expiresAt)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            StravaAthleteId = stravaAthleteId,
            HashedToken = hashedToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = expiresAt
        };
    }
}
