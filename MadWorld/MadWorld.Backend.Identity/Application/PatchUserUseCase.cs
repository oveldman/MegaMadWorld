using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.CommonExceptions;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class PatchUserUseCase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PatchUserUseCase(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<DefaultResponse> PatchUser(PatchUserRequest request)
    {
        ValidateAllRolesExists(request);
        
        var user = await RetrieveUser(request);
        await RemoveCurrentUserRoles(user);
        await AddNewRolesToUser(request, user);
        
        return new DefaultResponse();
    }

    private async Task<IdentityUser> RetrieveUser(PatchUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user;
    }

    private void ValidateAllRolesExists(PatchUserRequest request)
    {
        var existingRoles = _roleManager.Roles.Select(x => x.Name!).ToList();

        foreach (var role in request.Roles)
        {
            if (!existingRoles.Contains(role))
            {
                throw new RoleNotFoundException(role);
            }
        }
    }
    
    private async Task RemoveCurrentUserRoles(IdentityUser user)
    {
        var currentUserRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentUserRoles);
    }
    
    private async Task AddNewRolesToUser(PatchUserRequest request, IdentityUser user)
    {
        await _userManager.AddToRolesAsync(user, request.Roles);
    }
}