namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public sealed class PatchUserRequest
{
    public string Id { get; set; } = string.Empty;
    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
}