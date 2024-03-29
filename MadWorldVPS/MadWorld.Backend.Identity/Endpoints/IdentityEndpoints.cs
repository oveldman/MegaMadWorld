using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Asp.Versioning.Builder;
using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Extensions;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MadWorld.Backend.Identity.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var account = app.MapGroup("/Account")
            .WithTags("Account")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        account.MapIdentityApi<IdentityUserExtended>()
            .WithOpenApi()
            .HasApiVersion(1, 0);

        account.MapPost("/JwtLogin",
                ([FromBody] JwtLoginRequest request, [FromServices] PostJwtLoginUseCase useCase, HttpContext context) =>
                    useCase.PostJwtLogin(request, context.Request.GetOriginUrl()))
            .WithName("JwtLogin")
            .WithOpenApi()
            .AllowAnonymous()
            .HasApiVersion(1, 0);
        
        account.MapPost("/JwtRefresh",
                ([FromBody] JwtRefreshRequest request, [FromServices] PostJwtRefreshUseCase useCase, HttpContext context) =>
                    useCase.PostJwtRefresh(request, context.Request.GetOriginUrl()))
            .WithName("JwtRefresh")
            .WithOpenApi()
            .AllowAnonymous()
            .HasApiVersion(1, 0);
    }
}