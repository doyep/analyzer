using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Handles the user login process by redirecting to the Strava login page.
/// </summary>
public static class Login
{
    public static IResult Handle(IStravaAuthenticationService strava)
    {
        return Results.Redirect(strava.GenerateLoginUrl());
    }
}
