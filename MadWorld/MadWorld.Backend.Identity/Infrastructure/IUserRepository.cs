using MadWorld.Backend.Identity.Domain.Users;

namespace MadWorld.Backend.Identity.Infrastructure;

public interface IUserRepository
{
    List<IdentityUserExtended> GetUsers();
    Task AddRefreshToken(RefreshToken token);
    Task<int> DeleteExpiredRefreshTokens();
    RefreshToken? GetRefreshToken(string token);
}