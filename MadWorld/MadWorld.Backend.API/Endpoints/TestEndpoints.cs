using MadWorld.Shared.Contracts.Test;
using Microsoft.AspNetCore.Mvc;

namespace MadWorld.Backend.API.Endpoints;

public static class TestEndpoints
{
    public static void AddTestEndpoints(this WebApplication app)
    {
        var testEndpoint = app.MapGroup("/Test")
            .WithTags("Test")
            .RequireRateLimiting("GeneralLimiter");

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
                    HeaderIp = context.Request.Headers["X-Forwarded-For"]!,
                    RemoteIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
                })
            .WithName("WhatIsMyIp")
            .WithOpenApi()
            .AllowAnonymous();
    }
}