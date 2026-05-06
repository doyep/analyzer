namespace Doyep.Analyzer.Application.Auth;

public static class RefreshTokenErrors
{
    /// <summary>
    /// Indicates that there was a failure when trying to persist the refresh token to the database, which could
    /// be due to a database error or an issue with the token data itself.
    /// </summary>
    public static readonly Error FailedToPersist =
        new("refresh_token.failed_to_persist", "Failed to persist refresh token to the database.", 500);

    /// <summary>
    /// Indicates that the provided refresh token is invalid, which could be due to the token being malformed,
    /// expired, revoked, or not found in the database.
    /// </summary>
    public static readonly Error InvalidToken =
        new("refresh_token.invalid_token", "The provided refresh token is invalid.", 401);

    /// <summary>
    /// Indicates that a refresh token reuse was detected, which means that the same refresh token has been used
    /// more than once, suggesting a potential security issue such as token theft or replay attacks.
    /// </summary>
    public static readonly Error ReuseDetected =
        new("refresh_token.reuse_detected", "Refresh token reuse detected. The token has already been used or revoked.", 401);

    /// <summary>
    /// Indicates that the provided refresh token has been revoked, which means it is no longer valid for use in refreshing access tokens.
    /// This could occur if the token was manually revoked by the user or automatically revoked due to suspicious activity.
    /// </summary>
    public static readonly Error RevokedToken =
        new("refresh_token.revoked_token", "The provided refresh token has been revoked.", 401);

    /// <summary>
    /// Indicates that the provided refresh token has expired, which means it is no longer valid for use in refreshing access tokens.
    /// This could occur if the token has passed its expiration date and time, which is typically set when the token is issued based on the configured expiration duration.
    /// </summary>
    public static readonly Error ExpiredToken =
        new("refresh_token.expired_token", "The provided refresh token has expired.", 401);
}
