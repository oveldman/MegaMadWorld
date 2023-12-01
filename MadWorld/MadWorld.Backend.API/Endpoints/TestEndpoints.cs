namespace MadWorld.Backend.API.Endpoints;

public static class TestEndpoints
{
    public static void AddTestEndpoints(this WebApplication app)
    {
        var testEndpoint = app.MapGroup("/Test")
            .WithTags("Test");

        testEndpoint.MapGet("/Anonymous", () => "Hello Anonymous World!")
            .WithName("AnonymousTest")
            .WithOpenApi()
            .AllowAnonymous();

        testEndpoint.MapGet("/Authorized", () => "Hello Authorized World!")
            .WithName("AuthorizedTest")
            .WithOpenApi()
            .RequireAuthorization();
    }
}