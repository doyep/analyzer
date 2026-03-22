using Doyep.Analyzer.Application;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Api;

/// /// <summary>
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
    public static async Task<IResult> Handle(string? error, string? code, string? scope, IStravaAuthenticationService strava, IAthleteRepository athleteRepository)
    {
        // Handle access denied error, which occurs when the user cancels the authentication process or queryString are missing.
        var hasDeniedAccess = string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase);
        if (hasDeniedAccess || string.IsNullOrEmpty(code) || string.IsNullOrEmpty(scope)) return RedirectToLoginPage();

        // Verify the provided scope includes all required permissions. If not, return to the error page that explains the missing permissions.
        if (!ScopeValidator.HasRequiredScope(scope))
        {
            return RedirectToErrorPage("The permissions granted are insufisent to access the application");
        }

        // Exchange the authorization code for an access token and retrieve the athlete information from Strava. If the exchange fails, return to the error page that explains the failure.
        var stravaTokenResponse = await strava.ExchangeToken(code);
        if (stravaTokenResponse is null || stravaTokenResponse.Athlete is null)
        {
            return RedirectToErrorPage("Failed to exchange token with Strava. Please try again.");
        }

        // Check if the authenticated athlete is present in the whitelist. If not, register the athlete in the database, revoke the token and return to the error page that explains the situation.
        var stravaAthlete = stravaTokenResponse.Athlete;
        var dbAthlete = await athleteRepository.GetAthleteByStravaIdAsync(stravaAthlete.Id);
        if (dbAthlete is null)
        {
            dbAthlete = Athlete.Register(stravaAthlete.Id, stravaAthlete.Firstname, stravaAthlete.Lastname);
            await athleteRepository.AddAthleteAsync(dbAthlete);
            // TODO : Revoke the token immediately to prevent unauthorized access.
            return RedirectToErrorPage("Your Strava account is not authorized to access this application. Please contact the administrator.");
        }
        // TODO : Create JWT token and cookie session instead of returning the access token directly.
        // TODO : Redirect to the dashboard page instead of returning the token directly.
        return Results.Ok(stravaTokenResponse);
    }

    private static IResult RedirectToLoginPage() => Results.Redirect("/login");
    private static IResult RedirectToErrorPage(string message) => Results.Redirect($"/error?message={Uri.EscapeDataString(message)}");
}