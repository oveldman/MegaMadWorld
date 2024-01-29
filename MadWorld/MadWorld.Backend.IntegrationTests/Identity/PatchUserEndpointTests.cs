using System.Net;
using System.Net.Http.Json;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.IntegrationTests.Common;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class PatchUserEndpointTests : IdentityTestBase
{
    public PatchUserEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
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
            
            await context.SaveChangesAsync();
        }

        var client = Factory.CreateClient();
        
        // Act
        var request = new PatchUserRequest()
        {
            Id = userId.ToString(),
            IsBlocked = false,
            Roles = new []{ Roles.IdentityAdministrator, Roles.IdentityShipSimulator }
        };
        
        var response = await client.PatchAsJsonAsync("/UserManager/User", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        using (var scope = Factory.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>();
            var user = await userManager.FindByIdAsync(userId.ToString());
            user!.Email.ShouldBe(email);
            user.LockoutEnabled.ShouldBe(false);
            
            var roles = await userManager.GetRolesAsync(user);
            roles.Count.ShouldBe(2);
            roles.ShouldContain(Roles.IdentityAdministrator);
            roles.ShouldContain(Roles.IdentityShipSimulator);
        }
    }
}