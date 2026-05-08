using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure.Strava;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provide extension methods for registering Strava integration services.
/// This includes configuration options and the Strava authentication service.
/// </summary>
public static class StravaExtensions
{

    /// <summary>
    /// Register Strava integration services, such as the Strava authentication service.
    /// </summary>
    public static IServiceCollection AddStrava(this IServiceCollection services)
    {
        services.AddOptions<StravaOptions>()
            .BindConfiguration(StravaOptions.SectionName)
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<StravaOptions>, StravaOptionsValidator>();

        services.AddHttpClient<IStravaAuthService, StravaAuthService>((client) =>
        {
            client.BaseAddress = new Uri(StravaEndpoints.BaseUrl);
        });

        return services;
    }
}
