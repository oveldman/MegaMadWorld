using System.Runtime.CompilerServices;
using MadWorld.Backend.Application.CurriculaVitae;
using MadWorld.Backend.Application.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Application.CommonLogic.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.AddCurriculumVitaeApplication();
        
        builder.Services.AddScoped<GrpcTestUseCase>();
    }
    
    private static void AddCurriculumVitaeApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<GetCurriculumVitaeUseCase>();
    }
}