using MadWorld.Backend.Infrastructure.CurriculaVitae;
using MadWorld.Shared.Infrastructure.Databases;
using Microsoft.AspNetCore.Builder;

namespace MadWorld.Backend.Infrastructure.Database.Extensions;

public static class IApplicationBuilderExtensions
{
    public static void MigrateDatabases(this IApplicationBuilder app)
    {
        app.MigrateDatabase<CurriculaVitaeContext>();
    }
}