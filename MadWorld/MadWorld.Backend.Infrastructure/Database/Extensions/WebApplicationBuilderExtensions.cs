using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Infrastructure.Database.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.AddCurriculumVitaeDatabase();
    }

    private static void AddCurriculumVitaeDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CurriculaVitaeContext>(options =>
            options.UseNpgsql(
                builder.BuildConnectionString("CurriculaVitaeConnectionString"),
                b => b.MigrationsAssembly("MadWorld.Backend.Infrastructure")));
                
        
        builder.Services.AddScoped<ICurriculaVitaeRepository, CurriculaVitaeRepository>();
    }

    private static string BuildConnectionString(this WebApplicationBuilder builder, string connectionStringName)
    {
        var connectionString = builder.Configuration.GetValue<string>($"DbContext:{connectionStringName}")!;
        var password = builder.Configuration.GetValue<string>("DbContext:Password")!;
        return connectionString.Replace("{password}", password);
    }
}