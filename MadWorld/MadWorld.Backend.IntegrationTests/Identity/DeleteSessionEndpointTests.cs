using System.Net;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.IntegrationTests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class DeleteSessionEndpointTests : IdentityTestBase
{
    public DeleteSessionEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string email = "test@test.nl";
        
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            var adminUser = context.Users.First();
            
            var otherUser = new IdentityUserExtended()
            {
                Id = userId.ToString(),
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                NormalizedEmail = email,
                NormalizedUserName = email
            };
            
            await context.Users.AddAsync(otherUser);
            
            await context.RefreshTokens.AddAsync(new RefreshToken("Test", "https://test", new DateTime(2024, 01, 27, 0, 0,0,0, DateTimeKind.Utc), adminUser.Id));
            await context.RefreshTokens.AddAsync(new RefreshToken("Test2", "https://test", new DateTime(2024, 01, 29, 0, 0,0,0, DateTimeKind.Utc), otherUser.Id));
            
            await context.SaveChangesAsync();
        }
        
        var client = Factory.CreateClient();
        
        // Act
        var response = await client.DeleteAsync("/UserManager/Sessions?userId=" + userId);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            var result = context
                .RefreshTokens
                .AsNoTracking()
                .ToList();
            
            result.Count.ShouldBe(1);
            result[0].Token.ShouldBe("Test");
        }
    }
}