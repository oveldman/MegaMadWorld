using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.CommonExceptions;
using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class PatchUserUseCase
{
    private readonly UserManager<IdentityUserExtended> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PatchUserUseCase(UserManager<IdentityUserExtended> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<DefaultResponse> PatchUser(PatchUserRequest request)
    {
        ValidateAllRolesExists(request);
        
        var user = await RetrieveUser(request);
        await SetBlockedStatus(request, user);
        await RemoveCurrentUserRoles(user);
        await AddNewRolesToUser(request, user);
        
        return new DefaultResponse();
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

    private async Task<IdentityUserExtended> RetrieveUser(PatchUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user;
    }
    
    private async Task SetBlockedStatus(PatchUserRequest request, IdentityUserExtended user)
    {
        if (request.IsBlocked)
        {
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }
        else
        {
            await _userManager.SetLockoutEnabledAsync(user, false);
        }
    }
    
    private async Task RemoveCurrentUserRoles(IdentityUserExtended user)
    {
        var currentUserRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentUserRoles);
    }
    
    private async Task AddNewRolesToUser(PatchUserRequest request, IdentityUserExtended user)
    {
        await _userManager.AddToRolesAsync(user, request.Roles);
    }
}