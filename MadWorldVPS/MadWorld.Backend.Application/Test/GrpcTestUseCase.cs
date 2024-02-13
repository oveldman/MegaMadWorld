using MadWorld.Backend.Domain.Test;

namespace MadWorld.Backend.Application.Test;

public class GrpcTestUseCase
{
    private readonly ITestGrpcClient _client;

    public GrpcTestUseCase(ITestGrpcClient client)
    {
        _client = client;
    }
    
    public Task<TestGrpcData> GetTestGrpcData()
    {
        return _client.GetTestGrpcData();
    }
}