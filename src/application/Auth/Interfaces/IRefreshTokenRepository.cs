using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for managing refresh tokens for authenticated athletes.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Revokes all active refresh tokens associated with the specified Strava athlete ID.
    /// This is typically called when issuing a new refresh token to ensure that only one active refresh token exists per athlete at any given time.
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID for which to revoke active refresh tokens.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RevokeAllActiveByStravaAthleteIdAsync(long stravaAthleteId);

    /// <summary>
    /// Adds a new refresh token to the repository.
    /// </summary>
    /// <param name="refreshToken">The refresh token to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(RefreshToken refreshToken);
}
