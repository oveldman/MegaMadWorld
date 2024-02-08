namespace MadWorld.Backend.Identity.Domain.CommonExceptions;

public class RoleNotFoundException : Exception
{
    public string Role { get; init; }
    
    public RoleNotFoundException(string role)
    {
        Role = role;
    }
}