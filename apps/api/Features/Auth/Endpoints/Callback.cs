using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Application.Strava;

using Microsoft.AspNetCore.Mvc;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines the callback endpoint for handling the Strava OAuth flow.
/// Validates the request (error, state, scope, and authorization code),
/// exchanges the authorization code for Strava tokens, ensures the authenticated athlete is allowed access,
/// and on success issues HTTP-only access and refresh token authentication cookies and redirects the user to the configured frontend base URL (302).
/// If validation or authorization fails, the user is redirected to an appropriate error page.
/// </summary>
public static class Callback
{
    /// <summary>
    /// The full route for the callback endpoint, which is "/auth/callback". This is the URL that Strava will redirect to after the user authorizes the application,
    /// and it should match the redirect URI configured in the Strava API settings for this application.
    /// </summary>
    public static string FullPath => "/auth/callback";

    public static IEndpointRouteBuilder MapCallbackEndpoint(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/callback", async (
            string? error,
            string? code,
            string? scope,
            string? state,
            HttpContext context,
            [FromServices] IAuthCookieService authCookieService,
            [FromServices] ILoginService loginService,
            [FromServices] IStateService stateService) =>
        {
            var validationResult = ValidateCallbackRequest(error, code, scope, state);
            if (validationResult.IsFailure)
            {
                authCookieService.ClearAuthCookies(context);
                return Results.Redirect($"/error?code={validationResult.Error.Code}");
            }

            var stateResult = stateService.Consume(state!);
            if (stateResult.IsFailure)
            {
                authCookieService.ClearAuthCookies(context);
                return Results.Redirect($"/error?code={stateResult.Error.Code}");
            }

            var result = await loginService.LoginWithStravaAsync(code!, stateResult.Value.DeviceId);
            if (result.IsFailure)
            {
                authCookieService.ClearAuthCookies(context);
                return Results.Redirect($"/error?code={result.Error.Code}");
            }

            var tokens = result.Value;

            authCookieService.SetAuthCookies(context, tokens.JwtToken, tokens.RefreshToken);

            return Results.Redirect(stateResult.Value.RedirectUri.ToString() ?? "/");
        })
            .WithDescription("Callback endpoint for handling the Strava OAuth flow. Validates the request (error, state, scope, and authorization code), exchanges the authorization code for Strava tokens, ensures the authenticated athlete is allowed access, and on success issues HTTP-only access and refresh token authentication cookies and redirects the user to the configured frontend base URL (302). If validation or authorization fails, the user is redirected to an appropriate error page.")
            .Produces(StatusCodes.Status302Found);

        return app;
    }

    /// <summary>
    /// Validates the query parameters received from the Strava OAuth callback request, checking for the
    /// presence of an error parameter, ensuring the state parameter is valid, verifying that the required
    /// scope is included, and confirming that the authorization code is present. Returns a Result indicating
    /// success or failure with an appropriate error if validation fails.
    /// </summary>
    /// <param name="error">The error parameter returned by the Strava OAuth callback, if any.</param>
    /// <param name="code">The authorization code returned by the Strava OAuth callback.</param>
    /// <param name="scope">The scope parameter returned by the Strava OAuth callback.</param>
    /// <param name="state">The state parameter returned by the Strava OAuth callback.</param>
    /// <returns>A Result indicating success or failure with an appropriate error if validation fails.</returns>
    private static Result<Error> ValidateCallbackRequest(string? error, string? code, string? scope, string? state)
    {
        if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
            return Result<Error>.Failure(LoginErrors.AccessDenied);

        if (!string.IsNullOrEmpty(error))
            return Result<Error>.Failure(new Error("login.strava_error", $"Strava OAuth error: {error}", 500));

        if (string.IsNullOrEmpty(code))
            return Result<Error>.Failure(LoginErrors.MissingAuthorizationCode);

        if (string.IsNullOrEmpty(state))
            return Result<Error>.Failure(LoginErrors.InvalidState);

        if (!StravaAuthorizationScopes.HasRequiredScope(scope ?? string.Empty))
            return Result<Error>.Failure(LoginErrors.InvalidScope);

        return Result<Error>.Success();
    }
}
