namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Exception thrown when there is an error saving a Strava token to the database.
/// </summary>
public class StravaTokenPersistenceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StravaTokenPersistenceException"/> class with a message indicating the athlete ID for which the token could not be saved.
    /// </summary>
    public StravaTokenPersistenceException(long stravaAthleteId) : base($"Error saving token for athlete {stravaAthleteId}.")
    {
    }
}
