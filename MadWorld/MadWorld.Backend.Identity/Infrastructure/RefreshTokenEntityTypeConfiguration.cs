using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadWorld.Backend.Identity.Infrastructure;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Audience)
            .IsRequired()
            .HasMaxLength(RefreshToken.MaxLength);
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(RefreshToken.MaxLength);

        builder.Property(x => x.Expires)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(RefreshToken.MaxLength);
        
        builder
            .Navigation(e => e.User)
            .AutoInclude();

        builder.HasOne<IdentityUserExtended>(t => t.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(u => u.Id);
    }
}