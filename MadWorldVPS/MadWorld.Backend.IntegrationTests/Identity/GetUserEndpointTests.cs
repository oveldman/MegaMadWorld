using System.Net.Http.Json;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.IntegrationTests.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class GetUserEndpointTests : IdentityTestBase
{
    public GetUserEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        const string email = "test@test.nl";
        var userId = Guid.NewGuid();
        
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            var user = new IdentityUserExtended()
            {
                Id = userId.ToString(),
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                NormalizedEmail = email,
                NormalizedUserName = email
            };
            
            await context.Users.AddAsync(user);

            var currentRoles = context.Roles.ToList();
            foreach (var currentRole in currentRoles)
            {
                await context.UserRoles.AddAsync(new IdentityUserRole<string>()
                {
                    UserId = userId.ToString(),
                    RoleId = currentRole.Id
                });
            }

            await context.RefreshTokens.AddAsync(
                new RefreshToken("TestTestTest", "https://test",DateTime.UtcNow, userId.ToString()));
            
            await context.SaveChangesAsync();
        }

        var client = Factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"/UserManager/User?id={userId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetUserResponse>();
        result!.Id.ShouldBe(userId.ToString());
        result.Email.ShouldBe(email);
        result.IsBlocked.ShouldBe(false);
        result.Roles.Count.ShouldBe(2);
        result.Roles.ShouldContain("IdentityAdministrator");
        result.RefreshTokens.Count.ShouldBe(1);
        result.RefreshTokens[0].Id = "TestTestTest";
        result.RefreshTokens[0].Audience = "https://test";
    }
}