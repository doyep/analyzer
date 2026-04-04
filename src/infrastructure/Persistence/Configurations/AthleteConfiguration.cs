using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core configuration for the Athlete entity, defining how it maps to the database schema.
/// </summary>
public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
{
    /// <summary>
    /// Configures the Athlete entity's properties and relationships for Entity Framework Core.
    /// </summary>
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasKey(a => a.StravaAthleteId);
        builder.Property(a => a.StravaAthleteId)
            .ValueGeneratedNever();
        builder.Property(a => a.Role)
            .HasConversion<string>()
            .HasMaxLength(10);
    }
}
