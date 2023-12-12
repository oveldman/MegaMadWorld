namespace MadWorld.Backend.Identity.Endpoints;

public sealed class GetUsersResponse
{
    public IReadOnlyList<UserContract> Users { get; set; } = Array.Empty<UserContract>();
}