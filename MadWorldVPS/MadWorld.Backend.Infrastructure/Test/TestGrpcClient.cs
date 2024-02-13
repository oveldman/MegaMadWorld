using MadWorld.Backend.Domain.Test;

namespace MadWorld.Backend.Infrastructure.Test;

public class TestGrpcClient : ITestGrpcClient
{
    public TestGrpcData GetTestGrpcData()
    {
        return new TestGrpcData()
        {
            Message = "Hello from gRPC!"
        };
    }
}