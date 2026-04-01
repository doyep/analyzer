using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provides extension methods for registering frontend-related services and configurations to the dependency injection container.
/// </summary>
public static class FrontendExtensions
{
    /// <summary>
    /// Registers frontend-related services and configurations to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddFrontend(this IServiceCollection services)
    {
        services.AddOptions<FrontendOptions>()
            .BindConfiguration(FrontendOptions.SectionName)
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<FrontendOptions>, FrontendOptionsValidator>();

        return services;
    }
}
