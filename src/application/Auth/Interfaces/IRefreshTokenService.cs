using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for generating refresh tokens.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Issues a new refresh token for the specified Strava athlete ID. This method revokes all existing refresh tokens for the athlete, generates a new refresh token, hashes it, and creates a RefreshToken entity that can be stored in the database.
    /// The actual token value is returned to the caller, while the hashed version is stored for security reasons.
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID for which to issue the refresh token.</param>
    /// <returns>The newly issued refresh token.</returns>
    /// <exception cref="RefreshTokenPersistenceException">Thrown when there is an error while persisting the refresh token to the database.</exception>
    public Task<string> IssueRefreshTokenAsync(long stravaAthleteId);
}
