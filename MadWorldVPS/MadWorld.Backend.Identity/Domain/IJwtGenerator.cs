using MadWorld.Backend.Identity.Domain.Users;

namespace MadWorld.Backend.Identity.Domain;

public interface IJwtGenerator
{
    JwtToken GenerateToken(IdentityUserExtended user, string audience, IList<string> roles);
}