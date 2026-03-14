using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains the endpoint for handling Strava token exchange callback.
/// If the scope doesn't include all read permissions, it return an bad request response.
/// If the authenticated <see cref="Athlete"> is not present in the Whitelist, it return an unauthorized response.
/// If all checks pass, it return the access token.
/// </summary>
public static class ExchangeToken
{
    public static async Task<IResult> Handle(string code, string scope, IStravaAuthenticationService strava)
    {
        if (!ScopeValidator.HasRequiredScope(scope)) return Results.BadRequest("Missing required scope. Please ensure you have granted all necessary permissions.");
        var token = await strava.ExchangeToken(code);
        return token is not null ? Results.Ok(token) : Results.Unauthorized();
    }
}
