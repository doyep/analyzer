namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// This class contains extensions methods for mapping Authentication endpoints to the application.
/// </summary>
public static class AuthEndpoints
{
    /// <summary>
    /// Map authentication-related endpoints, such as login and callback, to the application.
    /// </summary>
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapLoginEndpoint();
        authGroup.MapCallbackEndpoint();
        authGroup.MapRefreshEndpoint();
        authGroup.MapLogoutEndpoint();

        return app;
    }
}
