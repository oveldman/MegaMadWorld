using System.Security.Claims;
using Asp.Versioning.Builder;
using MadWorld.Shared.Infrastructure.Settings;
using MadWorld.ShipSimulator.Application.Companies;
using MadWorld.ShipSimulator.Application.Danger;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.ShipSimulator.API.Endpoints;

public static class CompanyEndpoints
{
    public static void AddCompanyEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var companyEndpoints = app.MapGroup("Company")
            .WithTags("Company")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);
        
        companyEndpoints.MapGet("/", (HttpContext context, [FromServices] GetCompanyUseCase useCase) =>
            {
                var userId = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return useCase.GetCompanyAsync(userId);
            })
            .WithName("Company")
            .WithOpenApi()
            .RequireAuthorization(Policies.IdentityShipSimulator)
            .HasApiVersion(1, 0);
    }
}