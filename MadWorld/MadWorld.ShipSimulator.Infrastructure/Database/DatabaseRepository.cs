using MadWorld.ShipSimulator.Domain.Danger;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.ShipSimulator.Infrastructure.Database;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly ShipSimulatorContext _dbContext;

    public DatabaseRepository(ShipSimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public bool HardReset()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.Migrate();

        return true;
    }
}