namespace MadWorld.Backend.Identity.Contracts;

public sealed class JwtLoginResponse
{
    public bool IsSuccess { get; set; }
    public string Jwt { get; set; } = string.Empty;
}