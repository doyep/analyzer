namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines the contract for managing the authentication state during the OAuth flow with Strava.
/// This service is responsible for generating a unique state value for each authentication request
/// and validating it upon callback to prevent CSRF attacks and ensure the integrity of the authentication process.
/// </summary>
public interface IAuthStateService
{
    /// <summary>
    /// Generates a unique state value for the OAuth authentication request and stores it in cookies for later validation.
    /// The state value is typically a random string that helps to prevent CSRF attacks by ensuring that the callback received is in response to an authentication request initiated by the same client.
    /// </summary>
    string GenerateState(HttpContext context);

    /// <summary>
    /// Validates the state parameter received in the OAuth callback against the stored state value in cookies.
    /// Returns true if the state is valid, false otherwise.
    /// </summary>
    bool IsStateValid(string? state, HttpContext context);
}
