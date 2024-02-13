using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.Test;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using MadWorld.Backend.Infrastructure.Test;
using MadWorld.Shared.Infrastructure.Databases;
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
        
        builder.Services.AddScoped<ITestGrpcClient, TestGrpcClient>();
    }

    private static void AddCurriculumVitaeDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CurriculaVitaeContext>(options =>
            options.UseNpgsql(
                builder.BuildConnectionString("CurriculaVitaeConnectionString"),
                b => b.MigrationsAssembly("MadWorld.Backend.Infrastructure")));
                
        
        builder.Services.AddScoped<ICurriculaVitaeRepository, CurriculaVitaeRepository>();
    }
}