namespace MadWorld.Backend.Identity.Contracts;

public sealed class JwtLoginResponse
{
    public string Jwt { get; set; } = string.Empty;
}