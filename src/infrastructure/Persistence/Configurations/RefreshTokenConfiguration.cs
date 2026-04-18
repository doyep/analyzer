using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core configuration for the RefreshToken entity, defining how it maps to the database schema.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the RefreshToken entity's properties and relationships for Entity Framework Core.
    /// </summary>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Key
        builder.HasKey(rt => rt.Id);

        // Properties
        builder.Property(rt => rt.Id)
            .ValueGeneratedNever();

        builder.Property(rt => rt.StravaAthleteId)
            .IsRequired();

        builder.Property(rt => rt.HashedToken)
            .IsRequired()
            .HasMaxLength(44);

        builder.Property(rt => rt.CreatedAt)
            .IsRequired();

        builder.Property(rt => rt.ExpiresAt)
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);

        builder.Ignore(rt => rt.IsActive);

        // Indexes
        builder.HasIndex(rt => rt.ExpiresAt);

        builder.HasIndex(rt => rt.HashedToken)
            .IsUnique();

        builder.HasIndex(rt => new { rt.StravaAthleteId, rt.RevokedAt, rt.ExpiresAt });

        // Relations
        builder.HasOne<Athlete>()
            .WithMany()
            .HasForeignKey(rt => rt.StravaAthleteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
