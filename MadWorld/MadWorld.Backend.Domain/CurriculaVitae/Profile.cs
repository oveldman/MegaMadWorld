namespace MadWorld.Backend.Domain.CurriculaVitae;

public class Profile
{
    public const int MaxLength = 250;
    
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}