namespace MadWorld.Backend.Identity.Endpoints;

public sealed class JwtLoginResponse
{
    public string Jwt { get; set; } = string.Empty;
}