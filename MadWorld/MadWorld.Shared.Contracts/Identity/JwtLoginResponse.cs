namespace MadWorld.Shared.Contracts.Identity;

public sealed class JwtLoginResponse
{
    public bool IsSuccess { get; set; }
    public string Jwt { get; set; } = string.Empty;
}