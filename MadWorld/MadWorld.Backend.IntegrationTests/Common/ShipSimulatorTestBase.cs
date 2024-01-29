using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.ShipSimulator.API;
using MadWorld.ShipSimulator.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.Common;

public class ShipSimulatorTestBase : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    protected WebApplicationFactory<Program> Factory;
    
    protected readonly PostgreSqlContainer PostgreSqlContainer = PostgreSqlContainerBuilder.Build();

    public ShipSimulatorTestBase(WebApplicationFactory<Program> factory)
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
                services.RemoveAll(typeof(ShipSimulatorContext));
                services.RemoveAll(typeof(DbContextOptions<ShipSimulatorContext>));

                services.AddDbContext<ShipSimulatorContext>(options =>
                    options.UseNpgsql(PostgreSqlContainer.GetConnectionString()));

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                
                SetupServices(services);
            });
        });
    }

    protected virtual void SetupServices(IServiceCollection services)
    {
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