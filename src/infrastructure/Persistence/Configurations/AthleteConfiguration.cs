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
        // Key
        builder.HasKey(a => a.StravaAthleteId);

        // Properties
        builder.Property(a => a.StravaAthleteId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(a => a.FirstName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(a => a.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(10); // Ensure the length is never exceeded for the enum values

        builder.Property(a => a.LastConnection)
            .IsRequired(false);

        // Indexes
    }
}
