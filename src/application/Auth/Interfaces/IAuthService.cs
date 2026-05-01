namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for the authentication service responsible for handling user login and token management. This service interacts with the Strava API to authenticate users and generate the necessary tokens for accessing protected resources within the application.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Logs in a user using the provided Strava authorization code. This method exchanges the authorization code for an access token and a refresh token, and stores the refresh token in the database for future use. It also generates a JWT token for the authenticated user to access protected resources within the application.
    /// </summary>
    Task<Result<AuthTokens, Error>> LoginAsync(string authorizationCode, string state);

    /// <summary>
    /// Refreshes the access token using the provided refresh token. This method validates the refresh token, generates a new access token, and optionally issues a new refresh token. It also updates the stored refresh token in the database if a new one is issued. The method returns the new access token and refresh token (if applicable) to the caller.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use for generating a new access token.</param>
    /// <returns>A task representing the asynchronous operation, containing the new authentication tokens or an error.</returns>
    Task<Result<AuthTokens, Error>> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Logs out a user by invalidating the provided refresh token. This method revokes (blacklists) the refresh token from the database, effectively preventing the user from obtaining new access tokens using that refresh token in the future.
    /// </summary>
    /// <param name="refreshToken">The refresh token to revoke.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LogoutAsync(string refreshToken);
}
