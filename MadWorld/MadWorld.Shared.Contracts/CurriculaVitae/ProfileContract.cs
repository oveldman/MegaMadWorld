namespace MadWorld.Shared.Contracts.CurriculaVitae;

public class ProfileContract
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}