using MadWorld.Backend.Identity.Domain.Users;

namespace MadWorld.Backend.Identity.Infrastructure;

public interface IUserRepository
{
    List<IdentityUserExtended> GetUsers();
    Task AddRefreshToken(RefreshToken token);
    RefreshToken? GetRefreshToken(string token);
}