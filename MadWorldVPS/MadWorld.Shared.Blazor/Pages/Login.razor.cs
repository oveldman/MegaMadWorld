using Blazored.LocalStorage;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Shared.Blazor.Authentications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Shared.Blazor.Pages;

public partial class Login
{
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    [Inject]
    public IIdentityService IdentityService { get; set; } = null!;
    
    [Inject]
    public ILocalStorageService LocalStorage { get; set; } = null!;

    private JwtLoginRequest JwtLoginRequest { get; set; } = new();
    
    private bool _hasError;
    
    protected override async Task OnInitializedAsync()
    {
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
        await base.OnInitializedAsync();
    }

    private async Task LoginAsync()
    {
        _hasError = false;
        var response = await IdentityService.Login(JwtLoginRequest);

        if (response.IsSuccess)
        {
            await LocalStorage.SetItemAsync(LocalStorageKeys.JwtToken, response);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return;
        }
        
        _hasError = true;
    }
}