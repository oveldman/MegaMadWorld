using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Domain.Users;

public class RefreshToken
{
    public const int MaxLength = 1000;

    public RefreshToken(string token, string audience, DateTime expires, string userId)
    {
        Token = token;
        Audience = audience;
        Expires = expires;
        UserId = userId;
    }
    private RefreshToken() {}
    
    public Guid Id { get; set; }
    public string Audience { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public IdentityUserExtended User { get; set; } = null!;
}