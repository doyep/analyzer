using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api.Auth;

public static class ExchangeToken
{
    public static async Task<IResult> Handle(string authorizationCode, IStravaAuthenticationService strava)
    {
        var token = await strava.ExchangeToken(authorizationCode);
        return token is not null ? Results.Ok(token) : Results.Unauthorized();
    }

    public record Response(string Url);
}
