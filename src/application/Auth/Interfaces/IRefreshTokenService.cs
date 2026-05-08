namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for generating refresh tokens.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Issues a new refresh token for the specified Strava athlete ID and device ID. This method revokes all existing refresh tokens for the athlete, generates a new refresh token, hashes it, and creates a RefreshToken entity that can be stored in the database.
    /// The actual token value is returned to the caller, while the hashed version is stored for security reasons.
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID for which to issue the refresh token.</param>
    /// <param name="deviceId">The device ID for which to issue the refresh token.</param>
    /// <returns>The newly issued refresh token.</returns>
    /// <exception cref="RefreshTokenPersistenceException">Thrown when there is an error while persisting the refresh token to the database.</exception>
    Task<Result<string, Error>> IssueRefreshTokenAsync(long stravaAthleteId, Guid deviceId);

    /// <summary>
    /// Refreshes an access token using the provided raw refresh token. This method validates the refresh token, checks if it is active, and if valid, generates a new access token.
    /// It also handles the rotation of refresh tokens by revoking the old refresh token and issuing a new one.
    /// </summary>
    /// <param name="rawToken">The raw refresh token to use for refreshing the access token.</param>
    /// <returns>The result of the refresh operation, including the new refresh token and associated athlete.</returns>
    Task<Result<RefreshResult, Error>> RefreshAsync(string rawToken);

    /// <summary>
    /// Attempts to revoke the specified refresh token. This method checks if the provided refresh token exists and is active, and if so, it revokes the token to prevent further use. If the token does not exist or is already revoked,
    /// the method completes without throwing an exception, allowing for idempotent logout operations.
    /// </summary>
    /// <param name="refreshToken">The refresh token to revoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<Error>> RevokeAsync(string refreshToken);
}
