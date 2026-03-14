using Microsoft.Extensions.DependencyInjection;

using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure.Strava;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provide extension methods for registering Strava integration services.
/// This includes configuration options and the Strava authentication service.
/// </summary>
public static class StravaServiceCollectionExtensions
{

    /// <summary>
    /// Register Strava integration services, such as the Strava authentication service.
    /// </summary>
    public static IServiceCollection AddStrava(this IServiceCollection services)
    {
        services.AddHttpClient<IStravaAuthenticationService, StravaAuthenticationService>((client) =>
        {
            client.BaseAddress = new Uri(StravaEndpoints.BaseUrl);
        });

        return services;
    }
}
