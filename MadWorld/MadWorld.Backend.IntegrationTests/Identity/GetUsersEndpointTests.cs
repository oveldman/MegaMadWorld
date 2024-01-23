using System.Net.Http.Json;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using MadWorld.Backend.IntegrationTests.Common;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class GetUsersEndpointTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private WebApplicationFactory<Program> _factory;
    
    private readonly PostgreSqlContainer _postgreSqlContainer = PostgreSqlContainerBuilder.Build();

    public GetUsersEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        _factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(UserDbContext));
                services.RemoveAll(typeof(DbContextOptions<UserDbContext>));

                services.AddDbContext<UserDbContext>(options =>
                    options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
            });
        });
        
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            for (int i = 0; i < 12; i++)
            {
                var user = new IdentityUserExtended()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = $"test{i}@test.com",
                    UserName = $"test{i}@test.com",
                    EmailConfirmed = true,
                };
                
                await context.Users.AddAsync(user);
            }
            
            await context.SaveChangesAsync();
        }

        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/UserManager/Users?page=0");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetUsersResponse>();
        result!.Users.Count.ShouldBe(10);
    }
    
    public Task InitializeAsync()
    {
        return _postgreSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgreSqlContainer
            .DisposeAsync()
            .AsTask();
    }
}