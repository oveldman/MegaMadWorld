namespace MadWorld.Backend.Identity.Contracts;

public class JwtRefreshResponse
{
    public bool IsSuccess { get; init; }
    public DateTime Expires { get; init; }
    public string Jwt { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}