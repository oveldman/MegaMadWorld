using MadWorld.Backend.Application.CurriculaVitae.Mapper;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;

namespace MadWorld.Backend.Application.CurriculaVitae;

public class GetCurriculumVitaeUseCase
{
    private readonly ICurriculaVitaeRepository _curriculaVitaeRepository;

    public GetCurriculumVitaeUseCase(ICurriculaVitaeRepository curriculaVitaeRepository)
    {
        _curriculaVitaeRepository = curriculaVitaeRepository;
    }

    public GetCurriculumVitaeResponse GetCurriculumVitae(GetCurriculumVitaeRequest request)
    {
        var profile = _curriculaVitaeRepository.GetProfile(request.IsDraft);

        return new GetCurriculumVitaeResponse()
        {
            Profile = profile.ToDto()
        };
    }
}