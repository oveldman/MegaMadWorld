using System.Runtime.InteropServices.JavaScript;

namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public class GetUserResponse
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public required bool IsBlocked { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
    public IReadOnlyList<RefreshTokenContract> RefreshTokens { get; init; } = Array.Empty<RefreshTokenContract>();

    public static GetUserResponse Empty => new()
    {
        Id = string.Empty,
        Email = string.Empty,
        IsBlocked = false
    };
}