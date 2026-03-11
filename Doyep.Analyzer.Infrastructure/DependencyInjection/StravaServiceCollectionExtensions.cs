using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Doyep.Analyzer.Infrastructure.Strava;
using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Infrastructure;

public static class StravaServiceCollectionExtensions
{
    public static IServiceCollection AddStrava(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StravaOptions>(configuration.GetSection("Strava"));

        services.AddHttpClient<IStravaAuthenticationService, StravaAuthenticationService>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<StravaOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });

        return services;
    }
}
