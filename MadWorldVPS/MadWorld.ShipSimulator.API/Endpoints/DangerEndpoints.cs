using Asp.Versioning.Builder;
using MadWorld.Shared.Infrastructure.Settings;
using MadWorld.ShipSimulator.Application.Danger;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.ShipSimulator.API.Endpoints;

public static class DangerEndpoints
{
    public static void AddDangerEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var dangerEndpoints = app.MapGroup("Danger")
            .WithTags("Danger")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        dangerEndpoints.MapDelete("/HardReset", ([FromServices] PostHardResetUseCase useCase) =>
            {
                useCase.PostHardReset();

                return Results.Ok();
            })
            .WithName("HardReset")
            .WithOpenApi()
            .RequireAuthorization(Policies.IdentityAdministrator)
            .HasApiVersion(1, 0);
    }
}