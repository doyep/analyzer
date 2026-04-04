using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Provides services for managing athlete authentication and profile data.
/// </summary>
public class AthleteService(IAthleteRepository _athleteRepository) : IAthleteService
{
    /// <inheritdoc/>
    public async Task<Athlete> EnsureAthleteExistsAsync(StravaSummaryAthlete stravaAthlete)
    {
        var athlete = await _athleteRepository.FindByStravaAthleteIdAsync(stravaAthlete.Id);

        if (athlete is not null)
        {
            athlete.UpdateProfile(stravaAthlete.Firstname, stravaAthlete.Lastname);
            await _athleteRepository.UpdateAsync(athlete);
            return athlete;
        }

        athlete = Athlete.Register(stravaAthlete.Id, stravaAthlete.Firstname, stravaAthlete.Lastname);
        try
        {
            await _athleteRepository.AddAsync(athlete);
        }
        catch (DuplicateAthleteException)
        {
            athlete = await _athleteRepository.FindByStravaAthleteIdAsync(stravaAthlete.Id)
                ?? throw new InvalidOperationException("Failed to retrieve or create athlete.");
        }

        return athlete;
    }
}
