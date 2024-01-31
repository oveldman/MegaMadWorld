using System.Net.Http.Json;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class GetUsersEndpointTests : IdentityTestBase
{
    public GetUsersEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            for (var i = 0; i < 12; i++)
            {
                var email = $"test{i}@test.com";
                
                var user = new IdentityUserExtended()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    NormalizedEmail = email,
                    UserName = email,
                    NormalizedUserName = email,
                    EmailConfirmed = true,
                };
                
                await context.Users.AddAsync(user);
            }
            
            await context.SaveChangesAsync();
        }

        var client = Factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/UserManager/Users?page=0");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetUsersResponse>();
        result!.TotalCount.ShouldBe(13);
        result!.Users.Count.ShouldBe(10);
        result!.Users[1].Email.ShouldBe($"test0@test.com");
    }
}