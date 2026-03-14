namespace Doyep.Analyzer.Api.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapGet("/login", Login.Handle)
            .WithDescription("Redirects the user to the Strava login page.\n\nRedirections doesnt work in Scalar, you can't test this endpoint in this environment.")
            .Produces(StatusCodes.Status302Found);

        authGroup.MapGet("/token/{authorizationCode}", ExchangeToken.Handle);

        return app;
    }
}
