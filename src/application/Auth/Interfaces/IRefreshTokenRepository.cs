using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for managing refresh tokens for authenticated athletes.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Finds an active refresh token for the specified Strava athlete ID and device ID. An active refresh token is one that has not been revoked and has not expired.
    /// This method is typically used to check if there is an existing valid refresh token for a given athlete and device before issuing a new one.
    ///
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID for which to find an active refresh token.</param>
    /// <param name="deviceId">The device ID for which to find an active refresh token.</param>
    /// <returns>The active refresh token if found; otherwise, null.</returns>
    Task<RefreshToken?> FindActiveByStravaAthleteIdAndDeviceIdAsync(long stravaAthleteId, Guid deviceId);

    /// <summary>
    /// Adds a new refresh token to the repository.
    /// </summary>
    /// <param name="refreshToken">The refresh token to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(RefreshToken refreshToken);

    /// <summary>
    /// Revokes all active refresh tokens associated with the specified Strava athlete ID.
    /// This is typically called when issuing a new refresh token to ensure that only one active refresh token exists per athlete at any given time.
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID for which to revoke active refresh tokens.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// TODO : Proper token management strategy should allow one token per device or session.
    Task RevokeAllActiveByStravaAthleteIdAsync(long stravaAthleteId);

    /// <summary>
    /// Attempts to revoke a refresh token based on its hashed value. This method checks if the token exists, is not already revoked, and has not expired before revoking it.
    /// </summary>
    /// <param name="hashedRefreshToken">The hashed refresh token to revoke.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task TryRevokeAsync(string hashedRefreshToken);
}
