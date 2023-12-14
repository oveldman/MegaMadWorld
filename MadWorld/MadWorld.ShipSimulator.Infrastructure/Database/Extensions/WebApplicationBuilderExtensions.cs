using MadWorld.Shared.Infrastructure.Databases;
using MadWorld.ShipSimulator.Domain.Danger;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.ShipSimulator.Infrastructure.Database.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.AddShipSimulatorDatabase();
    }

    private static void AddShipSimulatorDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ShipSimulatorContext>(options =>
            options.UseNpgsql(
                builder.BuildConnectionString("ShipSimulatorConnectionString"),
                b => b.MigrationsAssembly("MadWorld.ShipSimulator.Infrastructure")));

        builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
    }
}