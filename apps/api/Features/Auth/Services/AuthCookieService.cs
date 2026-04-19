using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Api.Features.Auth;

public class AuthCookieService(
    IOptions<JwtOptions> jwtOptions,
    IOptions<RefreshTokenOptions> refreshTokenOptions
) : IAuthCookieService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly RefreshTokenOptions _refreshTokenOptions = refreshTokenOptions.Value;

    /// <inheritdoc/>
    public void SetAuthCookies(HttpContext context, string jwt, string refreshToken)
    {
        var jwtExpiration = DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);
        context.Response.Cookies.Append(CookieConstants.AccessToken, jwt, GetCookieOptions(jwtExpiration));

        var refreshTokenExpiration = DateTimeOffset.UtcNow.AddDays(_refreshTokenOptions.ExpirationInDays);
        context.Response.Cookies.Append(CookieConstants.RefreshToken, refreshToken, GetCookieOptions(refreshTokenExpiration));
    }

    /// <inheritdoc/>
    public void ClearAuthCookies(HttpContext context)
    {
        context.Response.Cookies.Append(CookieConstants.AccessToken, string.Empty, GetCookieOptions(DateTimeOffset.UtcNow.AddDays(-1)));

        context.Response.Cookies.Append(CookieConstants.RefreshToken, string.Empty, GetCookieOptions(DateTimeOffset.UtcNow.AddDays(-1)));
    }

    /// <inheritdoc/>
    public string? TryGetRefreshToken(HttpContext context)
    {
        context.Request.Cookies.TryGetValue(CookieConstants.RefreshToken, out var refreshToken);

        return refreshToken;
    }

    /// <summary>
    /// Generates a <see cref="CookieOptions"/> object with security attributes set for authentication cookies, including HttpOnly, Secure, SameSite, and an expiration date based on the provided parameter.
    /// This method centralizes the configuration of cookie options to ensure consistency across all authentication-related cookies.
    /// </summary>
    /// <param name="expires">The expiration date for the cookie.</param>
    /// <returns></returns>
    private static CookieOptions GetCookieOptions(DateTimeOffset expires)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = expires
        };
    }
}
