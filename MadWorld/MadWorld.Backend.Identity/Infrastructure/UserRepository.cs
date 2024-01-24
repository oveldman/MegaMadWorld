using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Identity.Infrastructure;

public class UserRepository : IUserRepository
{
    private int TakeAmount = 10;
    
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }
    
    public List<IdentityUserExtended> GetUsers(int page)
    {
        return _context
                .Users
                .AsNoTracking()
                .OrderBy(u => u.Email)
                .Skip(page * TakeAmount)
                .Take(TakeAmount)
                .ToList();
    }
    
    public Task AddRefreshToken(RefreshToken token)
    {
        _context
            .RefreshTokens
            .Add(token);
        
        return _context.SaveChangesAsync();
    }

    public Task<int> DeleteExpiredRefreshTokens()
    {
        return _context
            .RefreshTokens
            .Where(t => t.Expires < DateTime.UtcNow)
            .ExecuteDeleteAsync();
    }
    
    public RefreshToken? GetRefreshToken(string token)
    {
        return _context
                .RefreshTokens
                .AsNoTracking()
                .FirstOrDefault(t => t.Token == token);
    }
}