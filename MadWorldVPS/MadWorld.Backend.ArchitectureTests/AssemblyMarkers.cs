using ArchUnitNET.Loader;
using MadWorld.Backend.API;
using MadWorld.Backend.Application;
using MadWorld.Backend.Domain;
using MadWorld.Backend.Infrastructure;
using MadWorld.Shared.Contracts;

namespace MadWorld.Backend.ArchitectureTests;

public class AssemblyMarkers
{
    public static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(
            typeof(IBackendApiMarker).Assembly,
            typeof(IBackendApplicationMarker).Assembly,
            typeof(IBackendDomainMarker).Assembly,
            typeof(IBackendInfrastructureMarker).Assembly,
            typeof(ISharedContractsMarker).Assembly)
        .Build();

    public static readonly string BackendApiNamespace = typeof(IBackendApiMarker).Namespace!;
    public static readonly string BackendApplicationNamespace = typeof(IBackendApplicationMarker).Namespace!;
    public static readonly string BackendDomainNamespace = typeof(IBackendDomainMarker).Namespace!; 
    public static readonly string BackendInfrastructureNamespace = typeof(IBackendInfrastructureMarker).Namespace!;
    public static readonly string SharedContractsNamespace = typeof(ISharedContractsMarker).Namespace!;
}