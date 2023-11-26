namespace MadWorld.Shared.Contracts.CurriculaVitae;

public record GetCurriculumVitaeRequest
{
    public bool IsDraft { get; init; }
}