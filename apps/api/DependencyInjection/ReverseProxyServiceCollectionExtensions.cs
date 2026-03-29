namespace Doyep.Analyzer.Api;

public static class ReverseProxyServiceCollectionExtensions
{
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
