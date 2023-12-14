namespace MadWorld.Backend.Identity.Endpoints;

public sealed class UserContract
{
    public required string Id { get; init; }
    public required string Email { get; init; }
}