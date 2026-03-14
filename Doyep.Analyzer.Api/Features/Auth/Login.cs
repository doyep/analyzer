using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api.Auth;

/// <summary>
/// Provide endpoint that redirects to the login url for a Strava Application
/// </summary>
public static class Login
{
    public static IResult Handle(IStravaAuthenticationService strava)
    {
        var url = strava.GenerateLoginUrl();

        return Results.Redirect(url);
    }
}
