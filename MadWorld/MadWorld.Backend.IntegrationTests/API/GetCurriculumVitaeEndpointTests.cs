using System.Diagnostics.Metrics;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.API;

public class GetCurriculumVitaeEndpointTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    
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
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CurriculaVitaeContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                services.RemoveAll(typeof(DbContextOptions<CurriculaVitaeContext>));
                
                services.AddDbContext<CurriculaVitaeContext>(options =>
                    options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
            });
        }).CreateClient();

        var host = _factory.Server.Host;
        var context = host.Services.GetService<CurriculaVitaeContext>();
        context!.Profiles.Add(new Profile()
        {
            Id = new Guid("18d757f1-7352-41bb-9f8f-f1888465bb38"),
            FullName = "Emily Thompson",
            JobTitle = "Sales Manager"
        });
        await context.SaveChangesAsync();
        
        // Act
        var response = await client.GetAsync("/CurriculumVitae?isdraft=false");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", 
           response.Content.Headers.ContentType.ToString());
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