namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public class GetRolesResponse
{
    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
}