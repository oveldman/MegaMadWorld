using Asp.Versioning.Builder;
using MadWorld.Backend.Application.Test;
using MadWorld.Shared.Contracts.Test;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.API.Endpoints;

public static class TestEndpoints
{
    public static void AddTestEndpoints(this IVersionedEndpointRouteBuilder app)
    {
        var testEndpoint = app.MapGroup("/Test")
            .WithTags("Test")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        testEndpoint.MapGet("/Anonymous", () => "Hello Anonymous World!")
            .WithName("AnonymousTest")
            .WithOpenApi()
            .AllowAnonymous()
            .HasApiVersion(1, 0);

        testEndpoint.MapGet("/Authorized", () => "Hello Authorized World!")
            .WithName("AuthorizedTest")
            .WithOpenApi()
            .RequireAuthorization()
            .HasApiVersion(1, 0);
        
        testEndpoint.MapGet("/WhatIsMyIp", (HttpContext context) => 
                new GetWhatIsMyIpResponse()
                {
                    IpAddress = context.Request.Headers["X-Forwarded-For"]!
                })
            .WithName("WhatIsMyIp")
            .WithOpenApi()
            .AllowAnonymous()
            .HasApiVersion(1, 0);
        
        testEndpoint.MapGet("/gRPC", ([FromServices] GrpcTestUseCase useCase) => 
                useCase.GetTestGrpcData())
            .WithName("gRPC")
            .WithOpenApi()
            .AllowAnonymous()
            .HasApiVersion(1, 0);
    }
}