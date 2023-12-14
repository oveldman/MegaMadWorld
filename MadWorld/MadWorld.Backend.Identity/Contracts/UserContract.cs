namespace MadWorld.Backend.Identity.Contracts;

public sealed class UserContract
{
    public required string Id { get; init; }
    public required string Email { get; init; }
}