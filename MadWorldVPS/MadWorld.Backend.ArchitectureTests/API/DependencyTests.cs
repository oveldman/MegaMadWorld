namespace MadWorld.Backend.ArchitectureTests.API;

public class DependencyTests
{
    [Fact]
    public void ApplicationDependsNotOnInfrastructure()
    {
        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.BackendApplicationNamespace}.*", true)
            .Should().NotDependOnAny($"{AssemblyMarkers.BackendInfrastructureNamespace}.*", true);

        rule.Check(AssemblyMarkers.Architecture);
    }
    
    [Fact]
    public void DomainDependsNotOnBackendProjects()
    {
        var backendProjects = new List<string>()
        {
            $"{AssemblyMarkers.BackendApiNamespace}.*",
            $"{AssemblyMarkers.BackendApplicationNamespace}.*",
            $"{AssemblyMarkers.BackendInfrastructureNamespace}.*",
            $"{AssemblyMarkers.SharedContractsNamespace}.*"
        };

        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.BackendDomainNamespace}.*", true)
            .Should().NotDependOnAny(backendProjects, true);

        rule.Check(AssemblyMarkers.Architecture);
    }
}