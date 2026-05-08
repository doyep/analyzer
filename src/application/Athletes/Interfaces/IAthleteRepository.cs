using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Defines the contract for managing athlete data, including retrieval and persistence
/// of athlete information based on their Strava athlete ID.
/// </summary>
public interface IAthleteRepository
{
    /// <summary>
    /// Finds an athlete by their Strava athlete ID. Returns null if no athlete is found with the given ID.
    /// </summary>
    /// <param name="stravaAthleteId">The Strava athlete ID.</param>
    /// <returns>The athlete entity if found; otherwise, null.</returns>
    Task<Athlete?> FindByStravaAthleteIdAsync(long stravaAthleteId);

    /// <summary>
    /// Adds a new athlete to the repository. This method is used to persist a new athlete entity in the database.
    /// It is expected that the athlete entity being added has already been validated and does not violate any constraints.
    /// If an athlete with the same Strava athlete ID already exists, it throw an exception to indicate a conflict in the repository.
    /// The caller is responsible for handling such exceptions appropriately.
    /// </summary>
    /// <param name="athlete">The athlete entity to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="AthletePersistenceException">Thrown when an athlete with the same Strava athlete ID already exists in the repository.</exception>
    Task AddAsync(Athlete athlete);

    /// <summary>
    /// Updates an existing athlete in the repository. This method is used to persist changes to an existing athlete entity in the database.
    /// </summary>
    /// <param name="athlete">The athlete entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="AthletePersistenceException">Thrown when there is an issue updating the athlete in the repository.</exception>
    Task UpdateAsync(Athlete athlete);
}
