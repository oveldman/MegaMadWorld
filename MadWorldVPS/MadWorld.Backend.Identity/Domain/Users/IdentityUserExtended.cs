using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Domain.Users;

public class IdentityUserExtended : IdentityUser
{
    public IdentityUserExtended()
    {
    }
    
    public IdentityUserExtended(string userName) : base(userName)
    {
    }

    public virtual ICollection<RefreshToken> RefreshTokens { get; } = null!;
}