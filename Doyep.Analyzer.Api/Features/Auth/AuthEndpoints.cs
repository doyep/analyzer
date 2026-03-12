namespace Doyep.Analyzer.Api.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapGet("/login-url", GetLoginUrl.Handle);
        authGroup.MapGet("/token/{authorizationCode}", ExchangeToken.Handle);

        return app;
    }
}
