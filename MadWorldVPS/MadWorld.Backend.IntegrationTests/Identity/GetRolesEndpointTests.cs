using System.Net.Http.Json;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.IntegrationTests.Common;
using MadWorld.Shared.Infrastructure.Settings;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class GetRolesEndpointTests : IdentityTestBase
{
    public GetRolesEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        var client = Factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"/UserManager/Roles");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetRolesResponse>();
        result!.Roles.Count.ShouldBe(2);
        result!.Roles[0].ShouldBe(Roles.IdentityAdministrator);
        result!.Roles[1].ShouldBe(Roles.IdentityShipSimulator);
    }
}