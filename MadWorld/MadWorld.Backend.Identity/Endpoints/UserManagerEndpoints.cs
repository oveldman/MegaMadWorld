using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.Identity.Endpoints;

public static class UserManagerEndpoints
{
    public static void AddUserManagerEndpoints(this WebApplication app)
    {
        var userManager = app.MapGroup("/UserManager")
            .RequireRateLimiting("GeneralLimiter")
            .WithTags("UserManager");

        userManager.MapGet("/Users", ([FromServices] GetUsersUseCase userCase)
                => userCase.GetUsers())
            .RequireAuthorization(Policies.IdentityAdministrator);
    }
}