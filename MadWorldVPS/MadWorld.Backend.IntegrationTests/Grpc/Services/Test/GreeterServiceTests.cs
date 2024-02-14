using MadWorld.Backend.gRPC;
using MadWorld.Backend.IntegrationTests.Common;
using MadWorld.Shared.gRPC;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Grpc.Services.Test;

public class GreeterServiceTests : GrpcTestBase
{
    public GreeterServiceTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        var helloRequest = new HelloRequest()
        {
            Name = "Marco"
        };
        
        var greeterClient = new Greeter.GreeterClient(Channel);

        // Act
        var result = await greeterClient.SayHelloAsync(helloRequest);

        // Assert
        result.Message.ShouldBe($"Hello {helloRequest.Name}");
    }
}