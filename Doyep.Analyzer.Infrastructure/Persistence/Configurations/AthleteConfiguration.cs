using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doyep.Analyzer.Infrastructure.Persistence;

public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasKey(a => a.StravaId);
        builder.Property(a => a.StravaId)
            .ValueGeneratedNever();
        builder.Property(a => a.Role)
            .HasConversion<string>()
            .HasMaxLength(10);
    }
}