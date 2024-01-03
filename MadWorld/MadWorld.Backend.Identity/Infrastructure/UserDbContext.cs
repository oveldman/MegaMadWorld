using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Identity.Infrastructure;

public class UserDbContext : IdentityDbContext<IdentityUserExtended>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    
    public UserDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}