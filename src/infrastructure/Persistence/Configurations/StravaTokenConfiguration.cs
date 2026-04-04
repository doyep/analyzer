using Doyep.Analyzer.Application.Strava;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core configuration for the StravaToken entity, defining how it maps to the database schema.
/// </summary>
public class StravaTokenConfiguration : IEntityTypeConfiguration<StravaToken>
{
    public void Configure(EntityTypeBuilder<StravaToken> builder)
    {
        // Key
        builder.HasKey(t => t.StravaAthleteId);

        builder.Property(t => t.StravaAthleteId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(t => t.AccessToken)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(t => t.RefreshToken)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(t => t.ExpiresAt)
            .IsRequired();

        builder.Ignore(t => t.IsActive);

        // Indexes
    }
}
