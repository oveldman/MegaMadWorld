using MadWorld.Backend.API;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.Common;

public class ApiTestBase  : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    protected WebApplicationFactory<Program> Factory;
    
    protected readonly PostgreSqlContainer PostgreSqlContainer = PostgreSqlContainerBuilder.Build();

    public ApiTestBase(WebApplicationFactory<Program> factory)
    {
        Factory = factory;
        
        SetupFactory();
    }

    private void SetupFactory()
    {
        Factory = Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(CurriculaVitaeContext));
                services.RemoveAll(typeof(DbContextOptions<CurriculaVitaeContext>));

                services.AddDbContext<CurriculaVitaeContext>(options =>
                    options.UseNpgsql(PostgreSqlContainer.GetConnectionString()));
            });
        });
    }
    
    public Task InitializeAsync()
    {
        return PostgreSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return PostgreSqlContainer
            .DisposeAsync()
            .AsTask();
    }
}