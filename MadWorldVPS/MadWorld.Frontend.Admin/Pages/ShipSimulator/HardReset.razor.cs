using MadWorld.Frontend.Admin.Application.ShipSimulator.Reset;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.Admin.Pages.ShipSimulator;

public partial class HardReset
{
    [Inject]
    public IResetServerService ResetServerService { get; set; } = null!;

    private const string DefaultSuccessMessage = "Server has been reset.";
    private const string DefaultErrorMessage = "Something went wrong. Please try again later.";
    
    private bool ShowError;
    private bool ShowSuccess;

    private async Task ResetServer()
    {
        ResetMessages();
        
        var isSucceeded = await ResetServerService.Reset();
        ShowSuccess = isSucceeded;
        ShowError = !isSucceeded;
    }

    private void ResetMessages()
    {
        ShowSuccess = false;
        ShowError = false;
    }
}