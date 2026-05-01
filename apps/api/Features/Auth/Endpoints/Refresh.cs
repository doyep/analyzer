using Doyep.Analyzer.Application.Auth;

using Microsoft.AspNetCore.Mvc;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines the endpoint for refreshing JWT tokens using a valid refresh token.
/// The endpoint expects the refresh token to be sent in an HTTP-only cookie. It validates the refresh token,
/// and if valid, issues new JWT and refresh tokens, sets them in HTTP-only cookies, and returns a success response.
/// If the refresh token is invalid or expired, it clears the authentication cookies and returns an unauthorized response.
/// This endpoint allows clients to maintain user sessions without requiring the user to log in again, as long as they have a valid refresh token.
/// </summary>
public static class Refresh
{
    public static IEndpointRouteBuilder MapRefreshEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            HttpContext httpContext,
            [FromServices] IAuthCookieService _cookieService,
            [FromServices] IAuthService _authService) =>
        {
            httpContext.Request.Cookies.TryGetValue(CookieConstants.RefreshToken, out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return Results.BadRequest(new { error = "Invalid RefreshToken." });

            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result.IsFailure)
            {
                _cookieService.ClearAuthCookies(httpContext);
                return Results.Unauthorized();
            }

            var tokens = result.Value;
            _cookieService.SetAuthCookies(httpContext, tokens.JwtToken, tokens.RefreshToken);

            return Results.Ok(new { message = "Token refreshed successfully." });
        })
            .WithDescription("Endpoint for refreshing JWT tokens using a valid refresh token. The endpoint expects the refresh token to be sent in an HTTP-only cookie. It validates the refresh token, and if valid, issues new JWT and refresh tokens, sets them in HTTP-only cookies, and returns a success response. If the refresh token is invalid or expired, it clears the authentication cookies and returns an unauthorized response. This endpoint allows clients to maintain user sessions without requiring the user to log in again, as long as they have a valid refresh token.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        return app;
    }
}
