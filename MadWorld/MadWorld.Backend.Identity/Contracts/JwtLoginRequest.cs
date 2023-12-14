namespace MadWorld.Backend.Identity.Contracts;

public sealed class JwtLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}