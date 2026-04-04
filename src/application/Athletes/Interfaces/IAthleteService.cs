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
    /// <exception cref="AthleteAccessDeniedException">Thrown when the athlete does not have access to the application.</exception>
    /// <exception cref="AthleteNotFoundException">Thrown when the athlete cannot be found or created.</exception>
    public Task<Result<Athlete, Error>> GetAuthorizedAthleteAsync(StravaSummaryAthlete stravaAthlete);
}
