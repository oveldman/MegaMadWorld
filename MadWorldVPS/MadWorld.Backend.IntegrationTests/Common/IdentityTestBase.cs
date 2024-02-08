using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.BackgroundServices;
using MadWorld.Backend.Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.Common;

public class IdentityTestBase : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    protected WebApplicationFactory<Program> Factory;
    
    protected readonly PostgreSqlContainer PostgreSqlContainer = PostgreSqlContainerBuilder.Build();

    public IdentityTestBase(WebApplicationFactory<Program> factory)
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
                services.RemoveHostedService<DeleteSessionService>();
                services.RemoveAll(typeof(UserDbContext));
                services.RemoveAll(typeof(DbContextOptions<UserDbContext>));

                services.AddDbContext<UserDbContext>(options =>
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