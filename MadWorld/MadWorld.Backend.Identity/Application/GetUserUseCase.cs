using MadWorld.Backend.Identity.Application.Mappers;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.CommonExceptions;
using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Identity.Application;

public class GetUserUseCase
{
    private readonly UserManager<IdentityUserExtended> _userManager;

    public GetUserUseCase(UserManager<IdentityUserExtended> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<GetUserResponse> GetUser(string id)
    {
        var user = await _userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        var roles = await _userManager.GetRolesAsync(user);
        return user.ToDto(roles);
    }
}