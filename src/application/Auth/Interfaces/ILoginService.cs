namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for the login service responsible for handling user authentication with Strava.
/// This service provides methods to generate the Strava login URL and to process the login workflow by
/// exchanging the authorization code for authentication tokens.
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// Generates the Strava login URL for the user to initiate the authentication process.
    /// This URL will redirect the user to Strava's OAuth authorization page, where they can
    /// grant permissions to the application. The method takes a state parameter to maintain
    /// state between the request and callback, and a callback URI to which Strava will redirect
    /// after the user authorizes the application.
    /// </summary>
    /// <param name="state">The state parameter to maintain state between the request and callback.</param>
    /// <param name="callbackUri">The URI to which Strava will redirect after the user authorizes the application.</param>
    /// <returns>A URI that the user can visit to start the Strava authentication process.</returns>
    Uri GenerateStravaLoginUrl(string state, Uri callbackUri);

    /// <summary>
    /// Processes the login workflow by exchanging the provided Strava authorization code for authentication tokens.
    /// This method validates the authorization code and state, interacts with the Strava API
    /// to exchange the code for access and refresh tokens, and returns internal tokens if the exchange is successful.
    /// If the exchange fails, it returns an error result indicating the reason for the failure.
    /// </summary>
    /// <param name="authorizationCode">The authorization code received from Strava after the user has authorized the application.</param>
    /// <param name="deviceId">The unique identifier of the device initiating the login process.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains either the internal authentication tokens or an error.</returns>
    Task<Result<AuthTokens, Error>> LoginWithStravaAsync(string authorizationCode, Guid deviceId);
}
