using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;

namespace MadWorld.Backend.Application.CurriculaVitae.Mapper;

public static class ProfileMapper
{
    public static ProfileContract ToDto(this Profile profile)
    {
        return new ProfileContract()
        {
            Id = profile.Id,
            FullName = profile.FullName,
            JobTitle = profile.JobTitle
        };
    }
}