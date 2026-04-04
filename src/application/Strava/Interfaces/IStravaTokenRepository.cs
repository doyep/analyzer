namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Defines the contract for managing Strava authentication tokens in the repository.
/// </summary>
public interface IStravaTokenRepository
{
    /// <summary>
    /// Finds a StravaToken by the associated Strava athlete ID. Returns null if no token is found for the given athlete ID.
    /// The returned StravaToken will have its access and refresh tokens decrypted.
    /// </summary>
    Task<StravaToken?> FindByStravaAthleteId(long stravaAthleteId);

    /// <summary>
    /// Saves a StravaToken to the repository. If a token already exists for the given Strava athlete ID, it will be updated with the new values.
    /// The access and refresh tokens will be encrypted before being stored in the database.
    /// </summary>
    Task SaveAsync(StravaToken stravaToken);
}
