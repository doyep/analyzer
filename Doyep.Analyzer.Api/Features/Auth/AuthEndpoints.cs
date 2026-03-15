namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains extensions methods for mapping Authentication endpoints to the application.
/// </summary>
public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapGet("/login", Login.Handle)
            .WithDescription("Redirects the user to the Strava login page.\n\nRedirections doesnt work in Scalar, you can't test this endpoint in this environment.")
            .Produces(StatusCodes.Status302Found);

        authGroup.MapGet("/callback", Callback.Handle)
            .WithDescription("Callback endpoint for handling the Strava token exchanges. Exchanges the authorization code for an access token and a refresh token. While work in progress, it returns an bad request response if the authenticated user is not present in the whitelist, otherwise it return the access token.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}
