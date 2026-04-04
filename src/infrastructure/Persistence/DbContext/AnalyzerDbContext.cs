using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Domain;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Represents the Entity Framework Core database context for the Analyzer application.
/// </summary>
public class AnalyzerDbContext(
    DbContextOptions<AnalyzerDbContext> options,
    IEncryptionService? encryptionService = null)
    : DbContext(options), IDataProtectionKeyContext
{
    private readonly IEncryptionService? _encryptionService = encryptionService;

    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<StravaToken> StravaTokens => Set<StravaToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var converter = new ValueConverter<string, string>(
            v => _encryptionService != null ? _encryptionService.Encrypt(v) : v,
            v => _encryptionService != null ? _encryptionService.Decrypt(v) : v);

        modelBuilder.Entity<StravaToken>(entity =>
        {
            entity.Property(e => e.AccessToken)
                .HasConversion(converter);

            entity.Property(e => e.RefreshToken)
                .HasConversion(converter);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnalyzerDbContext).Assembly);
    }
}
