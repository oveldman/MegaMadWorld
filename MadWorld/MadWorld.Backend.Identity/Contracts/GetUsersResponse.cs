namespace MadWorld.Backend.Identity.Contracts;

public sealed class GetUsersResponse
{
    public IReadOnlyList<UserContract> Users { get; set; } = Array.Empty<UserContract>();
}