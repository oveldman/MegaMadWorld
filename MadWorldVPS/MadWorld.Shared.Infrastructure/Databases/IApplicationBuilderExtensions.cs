using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Shared.Infrastructure.Databases;

public static class IApplicationBuilderExtensions
{
    public static void MigrateDatabase<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
    
    public static string BuildConnectionString(this WebApplicationBuilder builder, string connectionStringName)
    {
        var connectionString = builder.Configuration.GetValue<string>($"DbContext:{connectionStringName}")!;
        var password = builder.Configuration.GetValue<string>("DbContext:Password")!;
        return connectionString.Replace("{password}", password);
    }
}