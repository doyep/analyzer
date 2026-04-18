namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for the authentication service responsible for handling user login and token management. This service interacts with the Strava API to authenticate users and generate the necessary tokens for accessing protected resources within the application.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Logs in a user using the provided Strava authorization code. This method exchanges the authorization code for an access token and a refresh token, and stores the refresh token in the database for future use. It also generates a JWT token for the authenticated user to access protected resources within the application.
    /// </summary>
    Task<Result<AuthTokens, Error>> LoginAsync(string authorizationCode);
}
