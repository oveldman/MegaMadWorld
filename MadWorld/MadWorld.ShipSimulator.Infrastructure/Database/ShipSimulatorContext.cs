using MadWorld.ShipSimulator.Domain.Companies;
using MadWorld.ShipSimulator.Infrastructure.Companies;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.ShipSimulator.Infrastructure.Database;

public class ShipSimulatorContext : DbContext
{
    public ShipSimulatorContext(DbContextOptions<ShipSimulatorContext> options) : base(options)
    {
    }
    
    public DbSet<Company> Companies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
    }
}