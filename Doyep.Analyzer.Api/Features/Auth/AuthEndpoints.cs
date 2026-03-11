using Doyep.Analyzer.Application.Strava;
using Microsoft.AspNetCore.Mvc;

namespace Doyep.Analyzer.Api;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapGet("/login-url", ([FromQuery] Uri redirectUri, IStravaAuthenticationService strava) =>
        {
            var authorizationUrl = strava.GenerateAuthorizationUrl(redirectUri);
            return TypedResults.Ok(new { AuthorizationUrl = authorizationUrl });
        });

        authGroup.MapGet("/token/{code}", async (string code, IStravaAuthenticationService strava) =>
        {
            return await strava.ExchangeToken(code);
        });

        return app;

    }
}
