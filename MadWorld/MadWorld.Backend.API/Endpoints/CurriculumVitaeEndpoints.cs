using MadWorld.Backend.Application.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;

namespace MadWorld.Backend.API.Endpoints;

public static class CurriculumVitaeEndpoints
{
    public static void AddCurriculumVitaeEndpoints(this WebApplication app)
    {
        app.MapGet("/CurriculumVitae", 
                (GetCurriculumVitaeRequest request, GetCurriculumVitaeUseCase useCase) => 
                    useCase.GetCurriculumVitae(request))
            .WithName("GetCurriculumVitae")
            .WithOpenApi();
    }
}