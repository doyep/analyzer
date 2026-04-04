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

    /// <summary>
    /// Logs out a user by revoking their access token and removing any associated refresh tokens from the database.
    /// This ensures that the user can no longer access protected resources until they log in again.
    /// The method takes the Strava athlete ID as a parameter to identify which user's tokens should be revoked and removed.
    /// </summary>
    Task LogoutAsync(long stravaAthleteId);
}
