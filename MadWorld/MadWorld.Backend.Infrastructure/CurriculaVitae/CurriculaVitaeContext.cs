using MadWorld.Backend.Domain.CurriculaVitae;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Infrastructure.CurriculaVitae;

public class CurriculaVitaeContext : DbContext
{
    public CurriculaVitaeContext(DbContextOptions<CurriculaVitaeContext> options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProfileEntityTypeConfiguration());
    }
}