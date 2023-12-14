using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.Backend.Identity.Infrastructure;

public static class IApplicationBuilderExtensions
{
    public static async Task AddFirstAdminAccountAsync(this IApplicationBuilder app)
    {
        const string defaultUser = "oveldman@gmail.com";
        
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        var user = await FindOrCreateUser(userManager, defaultUser);
        await FindOrCreateRole(roleManager, Roles.IdentityAdministrator);
        await FindOrCreateRole(roleManager, Roles.IdentityShipSimulator);
        await AddRoleToDefaultUser(userManager, user);
    }

    private static async Task<IdentityUser> FindOrCreateUser(UserManager<IdentityUser> userManager, string defaultUser)
    {
        var user = await userManager.FindByEmailAsync(defaultUser);
        if (user is not null)
        {
            return user;
        }

        var newUser = new IdentityUser(defaultUser)
        {
            EmailConfirmed = true,
            Email = defaultUser
        };
        await userManager.CreateAsync(newUser);
            
        return (await userManager.FindByEmailAsync(defaultUser))!;
    }
    
    private static async Task FindOrCreateRole(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            var newRole = new IdentityRole(roleName);
            await roleManager.CreateAsync(newRole);
        }
    }
    
    private static async Task AddRoleToDefaultUser(UserManager<IdentityUser> userManager, IdentityUser user)
    {
        var currentRoles = await userManager.GetRolesAsync(user);
        if (!currentRoles.Contains(Roles.IdentityAdministrator))
        {
            await userManager.AddToRoleAsync(user, Roles.IdentityAdministrator);   
        }
    }
}