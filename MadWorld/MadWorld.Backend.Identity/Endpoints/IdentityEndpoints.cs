using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Domain.Users;
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

        account.MapIdentityApi<IdentityUserExtended>()
            .WithOpenApi();

        account.MapPost("/JwtLogin",
                ([FromBody] JwtLoginRequest request, [FromServices] PostJwtLoginUseCase useCase) =>
                    useCase.PostJwtLogin(request))
            .WithName("JwtLogin")
            .WithOpenApi()
            .AllowAnonymous();
        
        account.MapPost("/JwtRefresh",
                ([FromBody] JwtRefreshRequest request, [FromServices] PostJwtRefreshUseCase useCase) =>
                    useCase.PostJwtRefresh(request))
            .WithName("JwtRefresh")
            .WithOpenApi()
            .AllowAnonymous();
    }
}