namespace MadWorld.Shared.Contracts.Test;

public sealed class GetWhatIsMyIpResponse
{
    public required string IpAddress { get; init; } = default!;
}