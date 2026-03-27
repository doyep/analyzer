using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Defines the contract for managing athlete authentication and profile data.
/// </summary>
public interface IAthleteService
{
    /// <summary>
    /// Ensures that an athlete corresponding to the given Strava summary athlete exists in the application.
    /// If the athlete already exists, their profile information is updated.
    /// If not, a new athlete is created.
    /// </summary>
    public Task<Athlete> EnsureAthleteExistsAsync(StravaSummaryAthlete stravaAthlete);
}
