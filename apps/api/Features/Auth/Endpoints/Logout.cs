using Doyep.Analyzer.Application.Auth;

using Microsoft.AspNetCore.Mvc;

namespace Doyep.Analyzer.Api.Features.Auth;

public static class Logout
{
    public static IEndpointRouteBuilder MapLogoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", async (
            HttpContext context,
            [FromServices] IAuthService authService,
            [FromServices] IAuthCookieService authCookieService) =>
        {
            var refreshToken = authCookieService.TryGetRefreshToken(context);
            if (refreshToken is null)
            {
                authCookieService.ClearAuthCookies(context);
                return Results.NoContent();
            }

            await authService.LogoutAsync(refreshToken);
            authCookieService.ClearAuthCookies(context);

            return Results.NoContent();
        });

        return app;
    }
}
