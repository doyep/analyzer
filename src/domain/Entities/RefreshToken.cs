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
    /// The identifier for the device or session associated with this refresh token. 
    /// This can be used to implement a token management strategy that allows one active refresh token per device or session.
    /// </summary>
    public Guid DeviceId { get; init; }

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
    /// The ID of the refresh token that replaced this token, if any. This can be used to track token rotation and ensure that when a new token is issued, the old token is revoked and linked to the new token for audit purposes.
    /// If null, this token has not been replaced.
    /// Note: The relationship between tokens (one-to-one or one-to-many) should be defined based on your application's requirements. In this implementation, we assume a one-to-one relationship where one token can only be replaced by one other token, but this can be adjusted if needed.
    /// </summary>
    public Guid? ReplacedByTokenId { get; private set; }

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

    /// <summary>
    /// Replaces the current refresh token with a new token by revoking the current token and setting the ReplacedByTokenId to the ID of the new token. This method is used during token rotation to ensure that when a new token is issued, the old token is revoked and linked to the new token for audit purposes.
    /// After calling this method, the current token will no longer be active, and the new token can be tracked as the replacement for this token. This helps maintain a clear history of token usage and ensures that old tokens cannot be used once a new token has been issued.
    /// </summary>
    /// <param name="newTokenId">The ID of the new refresh token that replaces the current token</param>
    public void ReplaceWith(Guid newTokenId)
    {
        Revoke();
        ReplacedByTokenId = newTokenId;
    }

    public static RefreshToken Create(long stravaAthleteId, Guid deviceId, string hashedToken, DateTimeOffset expiresAt)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            StravaAthleteId = stravaAthleteId,
            DeviceId = deviceId,
            HashedToken = hashedToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = expiresAt
        };
    }
}
