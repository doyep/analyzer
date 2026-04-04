using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure;
using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Handles the Strava OAuth token exchange callback.
/// If the user cancelled the authentication process, it returns a redirection to the login page.
/// If the provided scope does not include all required read permissions, a bad request response is returned.
/// If the authenticated <see cref="StravaSummaryAthlete"/> is not present in the whitelist, an unauthorized response is returned.
/// If all checks pass, the access token is returned.
///
/// TODO : Still work in progress.
/// </summary>
public static class Callback
{
    public static IEndpointRouteBuilder MapCallbackEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/callback", async (
            string? error,
            string? code,
            string? scope,
            string? state,
            HttpContext context,
            [FromServices] IAthleteService athleteService,
            [FromServices] IAuthStateService authStateService,
            [FromServices] IJwtTokenService tokenService,
            [FromServices] IStravaAuthenticationService stravaService,
            [FromServices] IStravaTokenRepository stravaTokenRepository,
            [FromServices] IOptions<FrontendOptions> frontendOptions,
            [FromServices] IOptions<JwtOptions> jwtOptions) =>
        {
            var appBaseUrl = frontendOptions.Value.BaseUrl;

            if (IsAccessDenied(error))
                return RedirectToErrorPage(appBaseUrl, AuthError.AccessDenied);

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(scope))
                return RedirectToErrorPage(appBaseUrl, AuthError.InvalidRequest);

            if (!authStateService.IsStateValid(state, context))
                return RedirectToErrorPage(appBaseUrl, AuthError.InvalidState);

            if (!ScopeValidator.HasRequiredScope(scope))
                return RedirectToErrorPage(appBaseUrl, AuthError.InvalidScope);

            var result = await HandleCallback(code, athleteService, stravaService, tokenService, stravaTokenRepository);

            if (!result.IsSuccess)
                return RedirectToErrorPage(appBaseUrl, result.Error);

            var expirationInMinutes = jwtOptions.Value.ExpirationInMinutes;
            context.Response.Cookies.Append(CookieConstants.AccessToken, result.Token!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
            });

            return Results.Redirect($"{appBaseUrl}/dashboard");
        })
            .WithDescription("Callback endpoint for handling the Strava OAuth flow. Validates the request (error, state, scope, and authorization code), exchanges the authorization code for Strava tokens, ensures the authenticated athlete is allowed access, and on success issues an HTTP-only authentication cookie and redirects the user to the /dashboard page (302). If validation or authorization fails, the user is redirected to an appropriate error page.")
            .Produces(StatusCodes.Status302Found);

        return app;
    }

    /// <summary>
    /// Handles the core logic of the Strava OAuth callback by exchanging the authorization code
    /// for an access token, ensuring the authenticated athlete exists in the database, checking
    /// if they have access permissions, and generating a JWT token for the authenticated user if
    /// all checks pass. If any step fails, an appropriate error result is returned to indicate the
    /// type of failure encountered during the authentication process.
    /// </summary>
    private static async Task<CallbackResult> HandleCallback(string code, IAthleteService athleteService, IStravaAuthenticationService strava, IJwtTokenService tokenService, IStravaTokenRepository stravaTokenRepository)
    {
        var stravaTokenResponse = await strava.ExchangeToken(code);
        if (stravaTokenResponse?.Athlete is null)
            return new CallbackResult { IsSuccess = false, Error = AuthError.StravaError };

        var dbAthlete = await athleteService.EnsureAthleteExistsAsync(stravaTokenResponse.Athlete);
        if (!dbAthlete.HasAccess())
        {
            await strava.Deauthorize(stravaTokenResponse.AccessToken);
            return new CallbackResult { IsSuccess = false, Error = AuthError.Unauthorized };
        }
        var stravaToken = StravaToken.CreateFrom(stravaTokenResponse);
        await stravaTokenRepository.SaveAsync(stravaToken);

        var jwtToken = tokenService.Generate(dbAthlete);
        return new CallbackResult { IsSuccess = true, Token = jwtToken };
    }

    /// <summary>
    /// Determines if the error parameter indicates that the user denied access during the Strava OAuth authentication process.
    /// </summary>
    private static bool IsAccessDenied(string? error)
        => string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Redirects the user to an appropriate error page based on the type of authentication
    /// error encountered during the Strava OAuth callback handling process. Each error type
    /// corresponds to a specific query parameter in the redirection URL, allowing the frontend
    /// to display relevant error messages to the user. If the error type is unrecognized, a generic
    /// error page is used as a fallback.
    /// </summary>
    private static IResult RedirectToErrorPage(string baseUrl, AuthError? error)
    {
        return error switch
        {
            AuthError.AccessDenied => Results.Redirect($"{baseUrl}/login"),
            AuthError.InvalidRequest => Results.Redirect($"{baseUrl}/error?code=INVALID_REQUEST"),
            AuthError.InvalidScope => Results.Redirect($"{baseUrl}/error?code=INVALID_SCOPE"),
            AuthError.InvalidState => Results.Redirect($"{baseUrl}/error?code=INVALID_STATE"),
            AuthError.StravaError => Results.Redirect($"{baseUrl}/error?code=STRAVA_ERROR"),
            AuthError.Unauthorized => Results.Redirect($"{baseUrl}/error?code=UNAUTHORIZED"),
            _ => Results.Redirect($"{baseUrl}/error")
        };
    }
}
