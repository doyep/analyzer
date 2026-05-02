using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
    public static IEndpointRouteBuilder MapCallbackEndpoint(this IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/callback", async (
            string? error,
            string? code,
            string? scope,
            string? state,
            HttpContext context,
            [FromServices] IAuthService authService,
            [FromServices] IAuthCookieService cookieService,
            [FromServices] ICallbackValidator validator,
            [FromServices] IOptions<FrontendOptions> frontendOptions) =>
        {
            var appBaseUrl = frontendOptions.Value.BaseUrl;

            var validationError = validator.ValidateCallbackRequest(error, code, scope, state);
            if (validationError is not null)
            {
                cookieService.ClearAuthCookies(context);
                return Results.Redirect($"{appBaseUrl}/error?code={validationError.Code}");
            }

            var result = await authService.LoginAsync(code!, state!);
            if (result.IsFailure)
            {
                cookieService.ClearAuthCookies(context);
                return Results.Redirect($"{appBaseUrl}/error?code={result.Error.Code}");
            }

            var tokens = result.Value;

            cookieService.SetAuthCookies(context, tokens.JwtToken, tokens.RefreshToken);

            return Results.Redirect(appBaseUrl);
        })
            .WithDescription("Callback endpoint for handling the Strava OAuth flow. Validates the request (error, state, scope, and authorization code), exchanges the authorization code for Strava tokens, ensures the authenticated athlete is allowed access, and on success issues HTTP-only access and refresh token authentication cookies and redirects the user to the configured frontend base URL (302). If validation or authorization fails, the user is redirected to an appropriate error page.")
            .Produces(StatusCodes.Status302Found);

        return app;
    }
}
