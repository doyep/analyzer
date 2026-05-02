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

/// <summary>
/// Exception thrown when a refresh token is reused, indicating that the token has already been used or revoked,
/// and cannot be used again for authentication.
/// </summary>
public class RefreshTokenReusalException : Exception
{
    public RefreshTokenReusalException() : base("The provided refresh token has already been used or revoked.")
    {
    }
}

/// <summary>
/// Exception thrown when a refresh token is revoked, indicating that the token has been explicitly invalidated and cannot be used for authentication.
/// This can occur when a user logs out or when a token is compromised and needs to be invalidated to prevent unauthorized access.
/// </summary>
public class RefreshTokenRevokedException : Exception
{
    public RefreshTokenRevokedException() : base("The provided refresh token has been revoked and cannot be used.")
    {
    }
}

/// <summary>
/// Exception thrown when a refresh token is expired, indicating that the token's validity period has ended and it can no longer be used for authentication.
/// This typically occurs when the token's expiration time has passed, and the user needs to re-authenticate to obtain a new refresh token and access token.
/// </summary>
public class RefreshTokenExpiredException : Exception
{
    public RefreshTokenExpiredException() : base("The provided refresh token has expired and cannot be used.")
    {
    }
}
