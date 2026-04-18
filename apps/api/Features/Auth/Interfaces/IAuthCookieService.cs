namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// Defines the contract for managing authentication cookies, including setting and clearing access token and refresh token cookies with appropriate security attributes.
/// </summary>
public interface IAuthCookieService
{
    /// <summary>
    /// Sets the authentication cookies (access token and refresh token) in the user's browser with appropriate security settings such as HttpOnly, Secure, and SameSite attributes.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="jwt">The JWT token.</param>
    /// <param name="refreshToken">The refresh token.</param>
    void SetAuthCookies(HttpContext context, string jwt, string refreshToken);

    /// <summary>
    /// Clears the authentication cookies from the user's browser, effectively logging the user out by removing the access token and refresh token cookies.
    /// This method should set the cookies with an expired date to ensure they are removed from the browser.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    void ClearAuthCookies(HttpContext context);
}
