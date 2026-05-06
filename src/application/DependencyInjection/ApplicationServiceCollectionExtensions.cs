using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Application.Auth;

using Microsoft.Extensions.DependencyInjection;

namespace Doyep.Analyzer.Application;

/// <summary>
/// Provides extension methods for registering application services to the dependency injection container.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// Registers application services to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAthleteService, AthleteService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<RedirectHostsValidator>();

        return services;
    }
}
