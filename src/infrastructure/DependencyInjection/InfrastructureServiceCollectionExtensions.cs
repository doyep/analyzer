using Microsoft.Extensions.DependencyInjection;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services in the dependency injection container.
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    /// <summary>
    /// Registers infrastructure services, such as authentication, persistence, and Strava API integration, to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddWeb()
            .AddTokenServices()
            .AddPersistence()
            .AddSecurity()
            .AddStrava();

        return services;
    }
}
