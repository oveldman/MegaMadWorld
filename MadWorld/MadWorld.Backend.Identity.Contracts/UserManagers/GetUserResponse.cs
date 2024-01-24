namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public class GetUserResponse
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
    public IReadOnlyList<RefreshTokenContract> RefreshTokens { get; init; } = Array.Empty<RefreshTokenContract>();
}