namespace MadWorld.Backend.Identity.Contracts;

public class GetRolesResponse
{
    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
}