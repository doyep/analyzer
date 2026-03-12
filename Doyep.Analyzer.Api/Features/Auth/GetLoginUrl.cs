using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api.Auth;

/// <summary>
/// Provide endpoint that generate the login url for a Strava Application
/// </summary>
public static class GetLoginUrl
{
    public static IResult Handle(Uri redirectUri, IStravaAuthenticationService strava)
    {
        var url = strava.GenerateAuthorizationUrl(redirectUri);

        return Results.Ok(new Response(url));
    }
    public record Response(string Url);
}
