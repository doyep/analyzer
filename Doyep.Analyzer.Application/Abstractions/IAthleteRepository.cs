using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application;

/// <summary>
/// Defines the contract for managing Athlete authentication data.
/// </summary>
public interface IAthleteRepository
{

    /// <summary>
    /// Retieves an Athlete by their unique Strava identifier. Returns null if no athlete is found.
    /// </summary>
    Task<Athlete?> GetAthleteByStravaIdAsync(long stravaId);

    /// <summary>
    /// Adds a new athlete to the repository. Used when a new athlete registers or is pre-authorized.
    /// </summary>
    Task AddAthleteAsync(Athlete athlete);
}