using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provides extension methods for registering web-related services and configurations to the dependency injection container.
/// </summary>
public static class WebExtensions
{
    /// <summary>
    /// Registers web-related services and configurations to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddOptions<WebOptions>()
            .BindConfiguration(WebOptions.SectionName)
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<WebOptions>, WebOptionsValidator>();

        return services;
    }
}
