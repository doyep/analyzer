using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Defines the contract for managing athlete authentication and profile data.
/// </summary>
public interface IAthleteService
{
    /// <summary>
    /// Ensures that an athlete exists in the database based on the provided Strava summary athlete information.
    /// If the athlete does not exist, it will be created.
    /// If the athlete already exists, their profile information will be updated.
    /// The method also checks if the athlete has access to the application.
    /// </summary>
    /// <param name="stravaAthlete">The Strava summary athlete information.</param>
    /// <returns>A result containing the athlete if successful, or an error if the athlete is not authorized or if there was an issue with finding or creating the athlete.</returns>
    public Task<Result<Athlete, Error>> GetAuthorizedAthleteAsync(StravaSummaryAthlete stravaAthlete);
}
