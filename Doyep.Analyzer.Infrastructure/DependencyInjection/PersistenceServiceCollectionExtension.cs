using Doyep.Analyzer.Application;
using Doyep.Analyzer.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AnalyzerDbContext>((serviceProvider, options) =>
        {
            var dbContextOptions = serviceProvider.GetRequiredService<IOptions<AnalyzerDbContextOptions>>().Value;
            options.UseNpgsql(dbContextOptions.AnalyzerDb);
        });

        services.AddScoped<IAthleteRepository, AthleteRepository>();

        return services;
    }
}
