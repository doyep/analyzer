using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api;

/// /// <summary>
/// Handles the Strava OAuth token exchange callback.
/// If the user cancelled the authentication process, it returns a redirection to the login page.
/// If the provided scope does not include all required read permissions, a bad request response is returned.
/// If the authenticated <see cref="Athlete"/> is not present in the whitelist, an unauthorized response is returned.
/// If all checks pass, the access token is returned.
/// 
/// TODO : Still work in progress.
/// </summary>
public static class Callback
{
    public static async Task<IResult> Handle(string code, string scope, IStravaAuthenticationService strava)
    {
        if (!ScopeValidator.HasRequiredScope(scope)) return Results.BadRequest("Missing required scope. Please ensure you have granted all necessary permissions.");
        var token = await strava.ExchangeToken(code);
        return token is not null ? Results.Ok(token) : Results.Unauthorized();
    }
}
