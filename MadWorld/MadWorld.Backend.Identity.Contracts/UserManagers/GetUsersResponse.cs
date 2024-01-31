namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public sealed class GetUsersResponse
{
    public int TotalCount { get; set; }
    public IReadOnlyList<UserContract> Users { get; set; } = Array.Empty<UserContract>();
}