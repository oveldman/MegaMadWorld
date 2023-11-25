using MadWorld.Backend.Infrastructure.CurriculaVitae;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Infrastructure.Database.Extensions;

public static class IApplicationBuilderExtensions
{
    public static void MigrateDatabases(this IApplicationBuilder app)
    {
        app.MigrateDatabase<CurriculaVitaeContext>();
    }
    
    private static void MigrateDatabase<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
}