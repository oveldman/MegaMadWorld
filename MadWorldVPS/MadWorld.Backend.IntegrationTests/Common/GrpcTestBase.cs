using Grpc.Net.Client;
using MadWorld.Backend.gRPC;
using Microsoft.AspNetCore.TestHost;

namespace MadWorld.Backend.IntegrationTests.Common;

public class GrpcTestBase : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private HttpMessageHandler? _handler;
    private GrpcChannel? _channel;
    
    protected WebApplicationFactory<Program> Factory;
    
    protected GrpcChannel Channel => _channel ??= CreateChannel();
    
    protected GrpcChannel CreateChannel()
    {
        return GrpcChannel.ForAddress(Factory.Server.BaseAddress, new GrpcChannelOptions
        {
            HttpHandler = _handler
        });
    }

    public GrpcTestBase(WebApplicationFactory<Program> factory)
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
            });
        });
        
        _handler = Factory.Server.CreateHandler();
    }
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}