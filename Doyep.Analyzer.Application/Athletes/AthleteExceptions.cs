namespace Doyep.Analyzer.Application.Athletes;

/// <summary>
/// Represents duplicate athlete exception, thrown when an athlete already exists in the repository.
/// </summary>
public class DuplicateAthleteException : Exception
{
    public DuplicateAthleteException(long stravaId) : base($"An athlete with Strava ID {stravaId} already exists.")
    {
    }
}

/// <summary>
/// Represents athlete not found exception, thrown when an attempt is made to access an athlete that does not exist in the repository.
/// </summary>
public class AthleteNotFoundException : Exception
{
    public AthleteNotFoundException(long stravaId) : base($"No athlete found with Strava ID {stravaId}.")
    {
    }
}
