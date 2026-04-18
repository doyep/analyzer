using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Domain;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Handles Jwt Token Generation for authenticated athletes.
/// </summary>
public class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
    private readonly JwtOptions _jwt = options.Value;

    /// <inheritdoc />
    public string Generate(Athlete athlete)
    {
        // here's a guide that use JwtRegisteredClaimNames instead of ClaimTypes :
        // https://medium.com/@solomongetachew112/jwt-authentication-in-net-8-a-complete-guide-for-secure-and-scalable-applications-6281e5e8667c
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, athlete.StravaAthleteId.ToString()),
            ..athlete.Role
                .GetInheritedRoles()
                .Select(role => new Claim(ClaimTypes.Role, role.ToString()))
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpirationInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
