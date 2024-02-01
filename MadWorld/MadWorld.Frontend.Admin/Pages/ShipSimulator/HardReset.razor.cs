using MadWorld.Frontend.Admin.Application.ShipSimulator.Reset;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.Admin.Pages.ShipSimulator;

public partial class HardReset
{
    [Inject]
    public IResetServerService ResetServerService { get; set; } = null!;
}