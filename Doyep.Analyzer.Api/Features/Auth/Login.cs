using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains the endpoint for handling user login.
/// The login workflow isn't handled by the API, this endpoint provides a redirection to the Strava login page.
/// </summary>
public static class Login
{
    public static IResult Handle(IStravaAuthenticationService strava)
    {
        return Results.Redirect(strava.GenerateLoginUrl());
    }
}
