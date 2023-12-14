using MadWorld.Backend.Application.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.API.Endpoints;

public static class CurriculumVitaeEndpoints
{
    public static void AddCurriculumVitaeEndpoints(this WebApplication app)
    {
        var curriculumVitaeEndpoints = app.MapGroup("")
            .WithTags("CurriculumVitae")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);
        
        curriculumVitaeEndpoints.MapGet("/CurriculumVitae",
                ([AsParameters] GetCurriculumVitaeRequest request, [FromServices] GetCurriculumVitaeUseCase useCase) =>
                    useCase.GetCurriculumVitae(request))
            .WithName("GetCurriculumVitae")
            .WithOpenApi()
            .AllowAnonymous();
    }
}