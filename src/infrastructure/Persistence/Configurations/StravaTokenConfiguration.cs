using Doyep.Analyzer.Application.Strava;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core configuration for the StravaToken entity, defining how it maps to the database schema.
/// </summary>
public class StravaTokenConfiguration : IEntityTypeConfiguration<EncryptedStravaToken>
{
    public void Configure(EntityTypeBuilder<EncryptedStravaToken> builder)
    {
        builder.HasKey(t => t.StravaAthleteId);
        builder.Property(t => t.StravaAthleteId)
            .ValueGeneratedNever();
    }
}
