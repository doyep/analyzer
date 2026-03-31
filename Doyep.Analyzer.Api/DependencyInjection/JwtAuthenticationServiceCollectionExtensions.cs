using System.Text;

using Doyep.Analyzer.Api.Features.Auth;
using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Provide extension methods for registering JWT authentication in the dependency injection container.
/// </summary>
public static class JwtAuthenticationServiceCollectionExtensions
{
    /// <summary>
    /// Register JWT authentication, including the configuration of JWT bearer options and token validation parameters.
    /// </summary>
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwt = configuration
            .GetRequiredSection(JwtOptions.SectionName)
            .Get<JwtOptions>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt!.Issuer,
                    ValidAudience = jwt!.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue(CookieConstants.AccessToken, out var accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
