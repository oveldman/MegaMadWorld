namespace MadWorld.Shared.Contracts.CurriculaVitae;

public record ProfileContract
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string JobTitle { get; init; } = string.Empty;
}