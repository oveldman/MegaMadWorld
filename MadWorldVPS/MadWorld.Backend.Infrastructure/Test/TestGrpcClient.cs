using Grpc.Net.Client;
using MadWorld.Backend.Domain.Options;
using MadWorld.Backend.Domain.Test;
using MadWorld.Shared.gRPC;
using Microsoft.Extensions.Options;

namespace MadWorld.Backend.Infrastructure.Test;

public class TestGrpcClient : ITestGrpcClient
{
    private readonly GrpcSettings _grpcSettings;
    
    public TestGrpcClient(IOptions<GrpcSettings> grpcSettings)
    {
        _grpcSettings = grpcSettings.Value;
    }
    
    public async Task<TestGrpcData> GetTestGrpcData()
    {
        using var channel = GrpcChannel.ForAddress(_grpcSettings.ConnectionString);
        var client = new Greeter.GreeterClient(channel);
        var reply = await client.SayHelloAsync(
            new HelloRequest { Name = "Final Master" });
        
        return new TestGrpcData()
        {
            Message = reply.Message
        };
    }
}