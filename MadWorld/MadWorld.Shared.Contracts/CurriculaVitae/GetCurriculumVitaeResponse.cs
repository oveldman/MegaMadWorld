namespace MadWorld.Shared.Contracts.CurriculaVitae;

public sealed record GetCurriculumVitaeResponse
{
    public ProfileContract Profile { get; init; } = null!;
}