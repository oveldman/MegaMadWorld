namespace MadWorld.Shared.Contracts.CurriculaVitae;

public record GetCurriculumVitaeResponse
{
    public ProfileContract Profile { get; init; } = null!;
}