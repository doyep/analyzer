using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Provide extension methods for registering JWT authentication in the dependency injection container.
/// </summary>
public static class JwtAuthenticationServiceCollectionExtensions
{
    /// <summary>
    /// Register JWT authentication, including the configuration of JWT bearer options and token validation parameters.
    /// </summary>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", _ => { });

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return services;
    }
}
