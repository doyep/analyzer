namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Represents athlete persistence exception, thrown when an error occurs while saving or updating an athlete in the repository.
/// </summary>
public class AthletePersistenceException : Exception
{
    public AthletePersistenceException(long stravaAthleteId) : base($"An error occurred while saving or updating the athlete with Strava ID {stravaAthleteId}.")
    {
    }
}
