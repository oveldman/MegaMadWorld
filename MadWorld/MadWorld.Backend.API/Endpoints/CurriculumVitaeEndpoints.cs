using MadWorld.Backend.Application.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.API.Endpoints;

public static class CurriculumVitaeEndpoints
{
    public static void AddCurriculumVitaeEndpoints(this WebApplication app)
    {
        var curriculumVitaeEndpoints = app.MapGroup("")
            .WithTags("CurriculumVitae")
            .RequireRateLimiting("GeneralLimiter");
        
        curriculumVitaeEndpoints.MapGet("/CurriculumVitae",
                ([AsParameters] GetCurriculumVitaeRequest request, [FromServices] GetCurriculumVitaeUseCase useCase) =>
                    useCase.GetCurriculumVitae(request))
            .WithName("GetCurriculumVitae")
            .WithOpenApi()
            .AllowAnonymous();
    }
}