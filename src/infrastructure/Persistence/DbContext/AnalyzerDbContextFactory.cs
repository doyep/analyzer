using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Doyep.Analyzer.Infrastructure.Persistence;

public class AnalyzerDbContextFactory
    : IDesignTimeDbContextFactory<AnalyzerDbContext>
{
    public AnalyzerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddUserSecrets<AnalyzerDbContextFactory>()
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AnalyzerDbContext>();

        var connectionString = configuration.GetConnectionString("DoyepAnalyzerDb");
        optionsBuilder.UseNpgsql(connectionString);

        return new AnalyzerDbContext(optionsBuilder.Options);
    }
}
