namespace MadWorld.Shared.Contracts.Test;

public sealed class GetWhatIsMyIpResponse
{
    public required string HeaderIp { get; init; } = default!;
    public required string RemoteIp { get; init; } = default!;
}