using Doyep.Analyzer.Api.Features.Auth;
using Doyep.Analyzer.Application.Strava;

using Microsoft.AspNetCore.Mvc;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Handles the user login process by redirecting to the Strava login page.
/// </summary>
public static class Login
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/login", async (
            HttpContext context,
            [FromServices] IAuthStateService authStateService,
            [FromServices] IStravaAuthenticationService stravaService) =>
        {
            var state = authStateService.GenerateState(context);
            return HandleGenerateLoginUrl(state, stravaService);
        })
            .WithDescription("Redirects the user to the Strava login page.\n\nRedirections doesnt work in Scalar, you can't test this endpoint in this environment.")
            .Produces(StatusCodes.Status302Found);

        return app;
    }

    private static IResult HandleGenerateLoginUrl(string state, IStravaAuthenticationService strava)
    {
        return Results.Redirect(strava.GenerateLoginUrl(state));
    }
}
