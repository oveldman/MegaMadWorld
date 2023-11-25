using MadWorld.Backend.Domain.CurriculaVitae;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadWorld.Backend.Infrastructure.CurriculaVitae;

public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(Profile.MaxLength);
        
        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(Profile.MaxLength);
    }
}