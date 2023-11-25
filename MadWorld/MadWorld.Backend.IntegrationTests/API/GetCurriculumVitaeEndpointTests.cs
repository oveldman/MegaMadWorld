using System.Net.Http.Json;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.API;

public class GetCurriculumVitaeEndpointTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private WebApplicationFactory<Program> _factory;
    
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithCleanUp(true)
        .Build(); 

    public GetCurriculumVitaeEndpointTests(WebApplicationFactory<Program> factory)
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
                services.RemoveAll(typeof(CurriculaVitaeContext));
                services.RemoveAll(typeof(DbContextOptions<CurriculaVitaeContext>));

                services.AddDbContext<CurriculaVitaeContext>(options =>
                    options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
            });
        });
        
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CurriculaVitaeContext>();

            await context.Profiles.AddAsync(new Profile()
            {
                Id = Guid.Parse("c6e2d4f0-8580-4f1c-9fd7-81c4a273b9a2"),
                FullName = "Emily Thompson",
                JobTitle = "Graphic Designer"
            });
            await context.SaveChangesAsync();
        }

        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/CurriculumVitae?isdraft=false");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetCurriculumVitaeResponse>();
        result!.Profile.Id.ShouldBe(Guid.Parse("c6e2d4f0-8580-4f1c-9fd7-81c4a273b9a2"));
        result!.Profile.FullName.ShouldBe("Emily Thompson");
        result!.Profile.JobTitle.ShouldBe("Graphic Designer");
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