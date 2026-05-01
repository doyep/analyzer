using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Defines the contract for managing Athlete authentication data.
/// </summary>
public interface IAthleteRepository
{
    /// <summary>
    /// Retrieves an Athlete by their unique Strava identifier.
    /// </summary>
    Task<Athlete?> FindByStravaAthleteIdAsync(long stravaAthleteId);

    /// <summary>
    /// Add a new athlete to the repository.
    /// </summary>
    Task AddAsync(Athlete athlete);

    /// <summary>
    /// Updates an existing athlete's information in the repository.
    /// </summary>
    Task UpdateAsync(Athlete athlete);
}
