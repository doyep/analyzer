namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Represents duplicate athlete exception, thrown when an athlete already exists in the repository.
/// </summary>
public class DuplicateAthleteException : Exception
{
    public DuplicateAthleteException(long stravaAthleteId) : base($"An athlete with Strava ID {stravaAthleteId} already exists.")
    {
    }
}

/// <summary>
/// Represents athlete not found exception, thrown when an attempt is made to access an athlete that does not exist in the repository.
/// </summary>
public class AthleteNotFoundException : Exception
{
    public AthleteNotFoundException(long stravaAthleteId) : base($"No athlete found with Strava ID {stravaAthleteId}.")
    {
    }
}
