using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Doyep.Analyzer.Infrastructure.Strava;
using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Infrastructure;

public static class StravaServiceCollectionExtensions
{
    public static IServiceCollection AddStravaService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StravaOptions>(configuration.GetSection("Strava"));

        services.AddHttpClient<IStravaService, StravaService>(client =>
        {
            client.BaseAddress = new Uri("https://www.strava.com/api/v3");
        });
        return services;
    }
}
