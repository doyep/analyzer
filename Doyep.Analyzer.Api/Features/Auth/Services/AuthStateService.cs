using System.Security.Cryptography;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Implements the <see cref="IAuthStateService"/> to manage the authentication state during the OAuth flow with Strava.
/// </summary>
public class AuthStateService : IAuthStateService
{
    /// <inheritdoc/>
    public string GenerateState(HttpContext context)
    {
        var state = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        context.Response.Cookies.Append(CookieConstants.StravaAuthState, state, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(10)
        });

        return state;
    }

    /// <inheritdoc/>
    public bool IsStateValid(string? state, HttpContext context)
    {
        if (string.IsNullOrEmpty(state))
            return false;

        context.Request.Cookies.TryGetValue(CookieConstants.StravaAuthState, out var storedState);
        var isValid = !string.IsNullOrEmpty(storedState) && state == storedState;

        if (isValid)
            context.Response.Cookies.Delete(CookieConstants.StravaAuthState);

        return isValid;
    }
}
