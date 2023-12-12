namespace MadWorld.Backend.Identity.Endpoints;

public sealed class JwtLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}