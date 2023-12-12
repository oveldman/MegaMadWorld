using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Infrastructure;

public interface IUserRepository
{
    List<IdentityUser> GetUsers();
}