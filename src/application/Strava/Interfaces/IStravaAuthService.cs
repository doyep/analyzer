namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Defines the contract for handling authentication and token management with the Strava API.
/// </summary>
public interface IStravaAuthService
{
    /// <summary>
    /// Generates the URL to redirect users to Strava's OAuth authorization page, including necessary query
    /// parameters such as client ID, redirect URI, response type, scope, and state token for CSRF protection.
    /// </summary>
    /// <param name="state">The state token used to prevent CSRF attacks.</param>
    /// <param name="callbackUri">The URI to which Strava will redirect after the user authorizes the application.</param>
    /// <returns>A URI that users should be redirected to for Strava authentication.</returns>
    Uri GenerateLoginUrl(string state, Uri callbackUri);

    /// <summary>
    /// Exchanges the authorization code received from Strava after user authentication for an access token and
    /// refresh token. This method sends a POST request to Strava's token endpoint with the required parameters
    /// and handles the response, returning either a successful token response or an error if the exchange fails.
    /// </summary>
    /// <param name="authorizationCode">The authorization code received from Strava.</param>
    /// <returns>A task representing the result of the token exchange, containing either the token response or an error.</returns>
    Task<Result<StravaAuthTokenResponse, Error>> ExchangeTokenAsync(string authorizationCode);

    /// <summary>
    /// Uses a refresh token to obtain a new access token from Strava when the current access token has expired.
    /// </summary>
    /// <param name="refreshToken">The refresh token used to obtain a new access token.</param>
    /// <returns>A task representing the result of the token refresh, containing either the new token response or an error.</returns>
    Task<Result<StravaRefreshTokenResponse, Error>> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Revoke the current access token from the application.
    /// </summary>
    /// <param name="accessToken">An active access token to be invalidated.</param>
    /// <returns>A task representing the asynchronous deauthorization process.</returns>
    Task<Result<Error>> Deauthorize(string accessToken);
}
