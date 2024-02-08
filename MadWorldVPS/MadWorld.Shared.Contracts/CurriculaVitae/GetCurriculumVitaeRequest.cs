namespace MadWorld.Shared.Contracts.CurriculaVitae;

public sealed record GetCurriculumVitaeRequest
{
    public bool IsDraft { get; init; }
}