using Blazored.LocalStorage;
using MadWorld.Shared.Blazor.Authentications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Shared.Blazor.Pages;

public partial class Login
{
    public bool IdentityAdministrator = false;
    
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    [Inject]
    public ILocalStorageService LocalStorage { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task LoginAsync()
    {
        const string token = "TempKey";
        
        await LocalStorage.SetItemAsStringAsync(LocalStorageKeys.JwtToken, token);
        var test = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        IdentityAdministrator = test.User.IsInRole(Roles.IdentityAdministrator);
    }
}