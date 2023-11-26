namespace MadWorld.Backend.Domain.CurriculaVitae;

public class Profile
{
    public const int MaxLength = 250;
    
    private Profile() {}

    public static Profile Create(string fullName, string jobTitle)
    {
        return new Profile()
        {
            FullName = fullName,
            JobTitle = jobTitle 
        };
    }
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string JobTitle { get; private set; } = string.Empty;
}