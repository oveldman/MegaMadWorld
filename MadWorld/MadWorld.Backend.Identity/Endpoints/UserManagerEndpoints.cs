using MadWorld.Backend.Identity.Application;
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

        userManager.MapGet("/Users", ([FromServices] GetUsersUseCase userCase)
                => userCase.GetUsers())
            .WithOpenApi();
    }
}