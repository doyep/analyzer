namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Exception thrown when there is an error while persisting a refresh token to the database.
/// </summary>
public class RefreshTokenPersistenceException : Exception
{
    public RefreshTokenPersistenceException() : base("An error occurred while persisting the refresh token.")
    {
    }
}
