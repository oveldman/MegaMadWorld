namespace MadWorld.Backend.Identity.Endpoints;

public class GetRolesResponse
{
    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
}