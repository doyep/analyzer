namespace Doyep.Analyzer.Api;

/// <summary>
/// Extension methods for adding reverse proxy services to the dependency injection container.
/// </summary>
public static class ReverseProxyServiceCollectionExtensions
{
    /// <summary>
    /// Adds reverse proxy services to the dependency injection container and configures them based on the application's configuration settings.
    /// </summary>
    public static IServiceCollection AddReverseProxy(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var reverseProxy = configuration
            .GetSection(ReverseProxyOptions.SectionName);

        services.AddReverseProxy()
            .LoadFromConfig(reverseProxy);

        return services;
    }
}
