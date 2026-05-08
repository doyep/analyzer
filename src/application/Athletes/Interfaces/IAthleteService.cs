using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Provides services for managing athlete authentication and profile data.
/// </summary>
public interface IAthleteService
{
    /// <summary>
    /// Ensures that an athlete exists in the system based on the provided Strava athlete data.
    /// If the athlete does not exist, it creates a new athlete. If the athlete already exists,
    /// it updates the existing record with the latest profile information from Strava.
    /// This method is used during the authentication process to ensure that the athlete's information
    /// is up-to-date and that the athlete is registered in the system. It returns the athlete entity that
    /// corresponds to the provided Strava athlete data, whether it was newly created or already existed.
    /// </summary>
    /// <param name="stravaAthlete">The Strava summary athlete information.</param>
    /// <returns>The athlete entity corresponding to the provided Strava athlete data, whether it was newly created or already existed.</returns>
    /// <exception cref="InvalidOperationException">Thrown when there is an issue finding or creating the athlete based on the provided Strava athlete data.</exception>
    Task<Athlete> EnsureAthleteAsync(StravaSummaryAthlete stravaAthlete);
}
