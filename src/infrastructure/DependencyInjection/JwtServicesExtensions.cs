using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provide extension methods for registering authentication-related services.
/// </summary>
public static class JwtServicesExtensions
{
    /// <summary>
    /// Register authentication-related services, such as the JWT token service and its configuration options.
    /// </summary>
    public static IServiceCollection AddJwtServices(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidator>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
