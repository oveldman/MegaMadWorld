using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.Identity.Endpoints;

public static class UserManagerEndpoints
{
    public static void AddUserManagerEndpoints(this WebApplication app)
    {
        var userManager = app.MapGroup("/UserManager")
            .WithTags("UserManager")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter)
            .RequireAuthorization(Policies.IdentityAdministrator);

        userManager.MapGet("/Users", ([FromQuery]int page,[FromServices] GetUsersUseCase userCase)
                => userCase.GetUsers(page))
            .WithOpenApi();
        
        userManager.MapGet("/User", ([FromQuery]string id, [FromServices] GetUserUseCase userCase)
                => userCase.GetUser(id))
            .WithOpenApi();
        
        userManager.MapPatch("/User", ([FromBody] PatchUserRequest request, [FromServices] PatchUserUseCase userCase)
                => userCase.PatchUser(request))
            .WithOpenApi();
        
        userManager.MapGet("/Roles", ([FromServices] GetRolesUseCase userCase)
                => userCase.GetRoles())
            .WithOpenApi();
    }
}