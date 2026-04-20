using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Implements the <see cref="ICallbackValidator"/> interface to provide validation logic for the Strava OAuth callback request parameters.
/// </summary>
public class CallbackValidator(
    IAuthStateService _authStateService
) : ICallbackValidator
{
    /// <inheritdoc/>
    public Error? ValidateCallbackRequest(string? error, string? code, string? scope, string? state, HttpContext context)
    {
        if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
            return AuthErrors.AccessDenied;

        if (!string.IsNullOrEmpty(error))
            return new Error("auth.oauth_error", $"OAuth error: {error}", 400);

        if (string.IsNullOrEmpty(code))
            return AuthErrors.MissingAuthorizationCode;

        if (!_authStateService.IsStateValid(state, context))
            return AuthErrors.InvalidState;

        if (!ScopeValidator.HasRequiredScope(scope ?? string.Empty))
            return AuthErrors.InvalidScope;

        return null;
    }
}
