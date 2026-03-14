using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure.Strava;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provide extension methodes for registering Strava integration services.
/// This includes configuration options and the Strava authentication service.
/// </summary>
public static class StravaServiceCollectionExtensions
{
    public static IServiceCollection AddStrava(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StravaOptions>(configuration.GetSection(StravaOptions.SectionName));
        services.AddHttpClient<IStravaAuthenticationService, StravaAuthenticationService>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<StravaOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}
