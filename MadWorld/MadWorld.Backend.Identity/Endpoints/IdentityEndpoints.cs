using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MadWorld.Backend.Identity.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this WebApplication app)
    {
        var account = app.MapGroup("/Account")
            .WithTags("Account")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        account.MapIdentityApi<IdentityUser>()
            .WithOpenApi();

        account.MapPost("/JwtLogin",
                ([FromBody] JwtLoginRequest request, [FromServices] GetJwtLoginUseCase useCase) =>
                    useCase.GetJwtLogin(request))
            .WithName("JwtLogin")
            .WithOpenApi()
            .AllowAnonymous();
    }
}