using System.Net;
using MadWorld.Backend.IntegrationTests.Common;
using MadWorld.ShipSimulator.API;
using MadWorld.ShipSimulator.Domain.Companies;
using MadWorld.ShipSimulator.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.ShipSimulator.Danger;

public class HardResetEndpointTests : ShipSimulatorTestBase
{
    public HardResetEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }


    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ShipSimulatorContext>();
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                UserId = Guid.NewGuid(),
                Amount = 1000,
            };
            
            await context.Companies.AddAsync(company);
            
            await context.SaveChangesAsync();
        }
        
        var client = Factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("/Danger/HardReset");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ShipSimulatorContext>();
            var companies = context.Companies.ToList();
            companies.Count.ShouldBe(0);
        }
    }
}