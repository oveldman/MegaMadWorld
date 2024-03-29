using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Shared.Common.Time;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Identity.Infrastructure;

public class UserRepository : IUserRepository
{
    private const int TakeAmount = 10;

    private readonly UserDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserRepository(UserDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public int CountUsers()
    {
        return _context
            .Users
            .Count();
    }
    
    public List<IdentityUserExtended> GetUsers(int page)
    {
        return _context
                .Users
                .AsNoTracking()
                .OrderBy(u => u.NormalizedEmail)
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
    
    public Task<int> DeleteUserSessions(string userId)
    {
        return _context
            .RefreshTokens
            .Where(t => t.UserId == userId)
            .ExecuteDeleteAsync();
    }

    public Task<int> DeleteExpiredRefreshTokens()
    {
        return _context
            .RefreshTokens
            .Where(t => t.Expires < _dateTimeProvider.UtcNow())
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