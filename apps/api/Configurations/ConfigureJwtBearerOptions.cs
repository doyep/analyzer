using System.Text;

using Doyep.Analyzer.Api.Features.Auth;
using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Configure JWT bearer options based on the application's JWT settings.
/// </summary>
public class ConfigureJwtBearerOptions(IOptions<JwtOptions> options) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwt = options.Value;

    /// <summary>
    /// Configure JWT bearer options for the specified authentication scheme. If the scheme is "Bearer", apply the JWT settings; otherwise, do nothing.
    /// </summary>
    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != "Bearer")
            return;

        Configure(options);
    }

    /// <summary>
    /// Configure JWT bearer options using the JWT settings from the configuration.
    /// This includes setting up token validation parameters and handling token retrieval from cookies.
    /// </summary>
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwt.Issuer,
            ValidAudience = _jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret)),
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue(AuthCookieConstants.AccessToken, out var accessToken))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    }
}
