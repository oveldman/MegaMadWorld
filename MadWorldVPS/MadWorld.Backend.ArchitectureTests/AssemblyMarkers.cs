using ArchUnitNET.Loader;
using MadWorld.Backend.API;
using MadWorld.Backend.Application;
using MadWorld.Backend.Domain;
using MadWorld.Backend.Infrastructure;
using MadWorld.Shared.Contracts;
using MadWorld.ShipSimulator.API;
using MadWorld.ShipSimulator.Application;
using MadWorld.ShipSimulator.Contracts;
using MadWorld.ShipSimulator.Domain;
using MadWorld.ShipSimulator.Infrastructure;

namespace MadWorld.Backend.ArchitectureTests;

public static class AssemblyMarkers
{
    public static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(
            typeof(IBackendApiMarker).Assembly,
            typeof(IBackendApplicationMarker).Assembly,
            typeof(IBackendDomainMarker).Assembly,
            typeof(IBackendInfrastructureMarker).Assembly,
            typeof(ISharedContractsMarker).Assembly,
            typeof(IShipSimulatorApiMarker).Assembly,
            typeof(IShipSimulatorApplicationMarker).Assembly,
            typeof(IShipSimulatorContractsMarker).Assembly,
            typeof(IShipSimulatorDomainMarker).Assembly,
            typeof(IShipSimulatorInfrastructureMarker).Assembly)
        .Build();

    public static readonly string BackendApiNamespace = typeof(IBackendApiMarker).Namespace!;
    public static readonly string BackendApplicationNamespace = typeof(IBackendApplicationMarker).Namespace!;
    public static readonly string BackendDomainNamespace = typeof(IBackendDomainMarker).Namespace!; 
    public static readonly string BackendInfrastructureNamespace = typeof(IBackendInfrastructureMarker).Namespace!;
    public static readonly string SharedContractsNamespace = typeof(ISharedContractsMarker).Namespace!;
    
    public static readonly string ShipSimulatorApiNamespace = typeof(IShipSimulatorApiMarker).Namespace!;
    public static readonly string ShipSimulatorApplicationNamespace = typeof(IShipSimulatorApplicationMarker).Namespace!;
    public static readonly string ShipSimulatorContractsNamespace = typeof(IShipSimulatorContractsMarker).Namespace!; 
    public static readonly string ShipSimulatorDomainNamespace = typeof(IShipSimulatorDomainMarker).Namespace!;
    public static readonly string ShipSimulatorInfrastructureNamespace = typeof(IShipSimulatorInfrastructureMarker).Namespace!;
}