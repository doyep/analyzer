using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for managing refresh tokens for authenticated athletes.
/// </summary>
public interface IRefreshTokenRepository
{
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
    /// Attempts to revoke the specified refresh token. This method checks if the provided refresh token exists and is active, and if so, it revokes the token to prevent further use. If the token does not exist or is already revoked,
    /// the method completes without throwing an exception, allowing for idempotent logout operations.
    /// </summary>
    /// <param name="refreshToken">The refresh token to revoke.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task TryRevokeAsync(string refreshToken);
}
