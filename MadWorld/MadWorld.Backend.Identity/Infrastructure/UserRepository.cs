using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }
    
    public List<IdentityUser> GetUsers()
    {
        return _context.Users.ToList();
    }
}