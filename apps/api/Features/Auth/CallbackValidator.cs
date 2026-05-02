using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Implements the <see cref="ICallbackValidator"/> interface to provide validation logic for the Strava OAuth callback request parameters.
/// </summary>
public class CallbackValidator : ICallbackValidator
{
    /// <inheritdoc/>
    public Error? ValidateCallbackRequest(string? error, string? code, string? scope, string? state)
    {
        if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
            return AuthErrors.AccessDenied;

        if (!string.IsNullOrEmpty(error))
            return new Error("auth.strava_error", $"Strava OAuth error: {error}", 500);

        if (string.IsNullOrEmpty(code))
            return AuthErrors.MissingAuthorizationCode;

        if (string.IsNullOrEmpty(state))
            return AuthErrors.InvalidState;

        if (!ScopeValidator.HasRequiredScope(scope ?? string.Empty))
            return AuthErrors.InvalidScope;

        return null;
    }
}
