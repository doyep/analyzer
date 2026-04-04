using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Provides services for managing athlete authentication and profile data.
/// </summary>
public class AthleteService(IAthleteRepository _athleteRepository) : IAthleteService
{
    /// <inheritdoc/>
    public async Task<Result<Athlete, Error>> GetAuthorizedAthleteAsync(StravaSummaryAthlete stravaAthlete)
    {
        var athlete = await FindOrCreateAthleteAsync(stravaAthlete);

        return athlete.HasAccess()
            ? Result<Athlete, Error>.Success(athlete)
            : Result<Athlete, Error>.Failure(AuthErrors.UnauthorizedAthlete);
    }

    /// <summary>
    /// Finds an existing athlete by their Strava athlete ID or creates a new one if it does not exist.
    /// If the athlete already exists, their profile information is updated with the latest data from Strava.
    /// If the athlete does not exist, a new athlete record is created in the database.
    /// The method returns the athlete entity regardless of whether it was found or created.
    /// </summary>
    /// <param name="stravaAthlete">The Strava athlete information.</param>
    /// <returns>The athlete entity.</returns>
    /// <exception cref="AthleteNotFoundException">Thrown when the athlete cannot be found or created.</exception>
    private async Task<Athlete> FindOrCreateAthleteAsync(StravaSummaryAthlete stravaAthlete)
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
                ?? throw new AthleteNotFoundException(stravaAthlete.Id);
        }
        return athlete;
    }
}
