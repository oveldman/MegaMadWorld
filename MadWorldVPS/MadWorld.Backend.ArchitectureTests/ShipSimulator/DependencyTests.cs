namespace MadWorld.Backend.ArchitectureTests.ShipSimulator;

public class DependencyTests
{
    [Fact]
    public void ApplicationDependsNotOnInfrastructure()
    {
        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.ShipSimulatorApplicationNamespace}.*", true)
            .Should().NotDependOnAny($"{AssemblyMarkers.ShipSimulatorInfrastructureNamespace}.*", true);

        rule.Check(AssemblyMarkers.Architecture);
    }
    
    [Fact]
    public void DomainDependsNotOnBackendProjects()
    {
        var backendProjects = new List<string>()
        {
            $"{AssemblyMarkers.ShipSimulatorApiNamespace}.*",
            $"{AssemblyMarkers.ShipSimulatorApplicationNamespace}.*",
            $"{AssemblyMarkers.ShipSimulatorInfrastructureNamespace}.*",
            $"{AssemblyMarkers.ShipSimulatorContractsNamespace}.*"
        };

        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.ShipSimulatorDomainNamespace}.*", true)
            .Should().NotDependOnAny(backendProjects, true);

        rule.Check(AssemblyMarkers.Architecture);
    }
}