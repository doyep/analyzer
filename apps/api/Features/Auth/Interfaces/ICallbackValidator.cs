using Doyep.Analyzer.Application;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines an interface for validating the parameters of the Strava OAuth callback request.
/// This includes checking for errors, validating the state parameter to prevent CSRF attacks, and ensuring that the required scopes are granted by the user.
/// Implementations of this interface can be used to centralize and encapsulate the validation logic for the callback endpoint, improving code organization and testability.
/// </summary>
public interface ICallbackValidator
{
    /// <summary>
    /// Validates the parameters of the Strava OAuth callback request. This method checks for the presence of an error parameter (indicating that the user denied access), validates the state parameter against the expected value stored in the session, and ensures that the scope parameter includes all required scopes for the application to function properly. If any validation fails, an appropriate <see cref="Error"/> is returned; otherwise, null is returned to indicate that the request is valid.
    /// </summary>
    /// <param name="error">The error parameter from the callback request.</param>
    /// <param name="code">The authorization code from the callback request.</param>
    /// <param name="scope">The scope parameter from the callback request.</param>
    /// <param name="state">The state parameter from the callback request.</param>
    /// <param name="context">The HTTP context for the callback request.</param>
    /// <returns>An <see cref="Error"/> if validation fails, otherwise null.</returns>
    Error? ValidateCallbackRequest(string? error, string? code, string? scope, string? state, HttpContext context);
}
