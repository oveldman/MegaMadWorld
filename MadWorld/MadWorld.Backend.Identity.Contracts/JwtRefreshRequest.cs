namespace MadWorld.Backend.Identity.Contracts;

public sealed class JwtRefreshRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}