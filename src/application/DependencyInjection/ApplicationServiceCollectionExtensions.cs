using Doyep.Analyzer.Application.Athletes;

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

        return services;
    }
}
