using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Provide extension methods for registering persistence-related services.
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    /// Register persistence-related services, such as the database context and repositories.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<AnalyzerDbContextOptions>()
            .BindConfiguration(AnalyzerDbContextOptions.SectionName)
            .ValidateOnStart();

        services.AddDbContext<AnalyzerDbContext>((sp, options) =>
        {
            var dbContextOptions = sp.GetRequiredService<IOptions<AnalyzerDbContextOptions>>().Value;
            options.UseNpgsql(dbContextOptions.DoyepAnalyzerDb, npgsql =>
            {
                npgsql.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });
        });

        services.AddScoped<IAthleteRepository, AthleteRepository>();

        return services;
    }
}
