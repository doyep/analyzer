using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Handles the user login process by redirecting to the Strava login page.
/// </summary>
public static class Login
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/login", async (
            string deviceId,
            [FromServices] IStravaAuthenticationService stravaService,
            [FromServices] IOptions<FrontendOptions> frontendOptions) =>
        {
            var appBaseUrl = frontendOptions.Value.BaseUrl;

            if (!Guid.TryParse(deviceId, out var deviceGuid))
                return Results.Redirect($"{appBaseUrl}/error?code=invalid_device_id");

            return Results.Redirect(stravaService.GenerateLoginUrl(deviceGuid));
        })
            .WithDescription("Redirects the user to the Strava login page.\n\nRedirections doesnt work in Scalar, you can't test this endpoint in this environment.")
            .Produces(StatusCodes.Status302Found);

        return app;
    }
}
