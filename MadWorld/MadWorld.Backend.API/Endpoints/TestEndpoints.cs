using MadWorld.Shared.Contracts.Test;
using MadWorld.Shared.Infrastructure.Settings;

namespace MadWorld.Backend.API.Endpoints;

public static class TestEndpoints
{
    public static void AddTestEndpoints(this WebApplication app)
    {
        var testEndpoint = app.MapGroup("/Test")
            .WithTags("Test")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        testEndpoint.MapGet("/Anonymous", () => "Hello Anonymous World!")
            .WithName("AnonymousTest")
            .WithOpenApi()
            .AllowAnonymous();

        testEndpoint.MapGet("/Authorized", () => "Hello Authorized World!")
            .WithName("AuthorizedTest")
            .WithOpenApi()
            .RequireAuthorization();
        
        testEndpoint.MapGet("/WhatIsMyIp", (HttpContext context) => 
                new GetWhatIsMyIpResponse()
                {
                    IpAddress = context.Request.Headers["X-Forwarded-For"]!
                })
            .WithName("WhatIsMyIp")
            .WithOpenApi()
            .AllowAnonymous();
    }
}