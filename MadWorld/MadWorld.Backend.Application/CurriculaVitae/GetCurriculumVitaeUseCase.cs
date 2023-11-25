using MadWorld.Backend.Application.CurriculaVitae.Mapper;
using MadWorld.Backend.Domain.CommonExceptions;
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
        if (request is null)
            throw new ValidationException(nameof(GetCurriculumVitaeRequest));
        
        var profile = _curriculaVitaeRepository.GetProfile(request.IsDraft);

        if (profile is null)
            throw new EntityNotFoundException(nameof(profile));
        
        return new GetCurriculumVitaeResponse()
        {
            Profile = profile.ToDto()
        };
    }
}