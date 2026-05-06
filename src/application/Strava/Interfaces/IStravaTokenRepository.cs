namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Defines the contract for a repository responsible for managing Strava authentication tokens associated with athletes.
/// </summary>
public interface IStravaTokenRepository
{
    /// <summary>
    /// Finds a StravaToken entity based on the given Strava athlete ID.
    /// </summary>
    /// <param name="stravaAthleteId">The ID of the Strava athlete whose tokens are being retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing the StravaToken if found, or null if not found.</returns>
    Task<StravaToken?> FindByStravaAthleteIdAsync(long stravaAthleteId);

    /// <summary>
    /// Saves the given StravaToken to the repository. This method should handle both creating new tokens and updating
    /// existing ones based on the Strava athlete ID.
    /// </summary>
    /// <param name="stravaToken">The StravaToken instance containing the authentication tokens and associated athlete ID.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    /// <exception cref="StravaTokenPersistenceException">Thrown when there is an error persisting the Strava token.</exception>
    Task SaveAsync(StravaToken stravaToken);
}
