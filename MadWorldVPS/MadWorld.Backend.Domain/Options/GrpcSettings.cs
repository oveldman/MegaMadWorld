namespace MadWorld.Backend.Domain.Options;

public class GrpcSettings
{
    public const string SectionName = "Grpc";
    
    public string ConnectionString { get; set; } = string.Empty;
}