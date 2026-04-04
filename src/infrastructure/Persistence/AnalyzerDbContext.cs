using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Represents the Entity Framework Core database context for the Analyzer application.
/// </summary>
public class AnalyzerDbContext(DbContextOptions<AnalyzerDbContext> options) : DbContext(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<EncryptedStravaToken> StravaTokens => Set<EncryptedStravaToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnalyzerDbContext).Assembly);
    }
}
