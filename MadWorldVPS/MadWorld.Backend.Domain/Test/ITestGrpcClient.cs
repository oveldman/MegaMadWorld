namespace MadWorld.Backend.Domain.Test;

public interface ITestGrpcClient
{
    Task<TestGrpcData> GetTestGrpcData();
}