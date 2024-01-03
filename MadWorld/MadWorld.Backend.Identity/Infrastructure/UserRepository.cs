using MadWorld.Backend.Identity.Domain.Users;

namespace MadWorld.Backend.Identity.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }
    
    public List<IdentityUserExtended> GetUsers()
    {
        return _context.Users.ToList();
    }
    
    public Task AddRefreshToken(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        return _context.SaveChangesAsync();
    }
    
    public RefreshToken? GetRefreshToken(string token)
    {
        return _context.RefreshTokens.FirstOrDefault(t => t.Token == token);
    }
}