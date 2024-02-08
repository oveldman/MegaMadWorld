using Blazored.LocalStorage;
using MadWorld.Shared.Blazor.Authentications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Shared.Blazor.Pages;

public partial class Logout
{
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    [Inject]
    public IAccessTokenWriter AccessTokenWriter { get; set; } = null!;
    
    [Inject]
    public ILocalStorageService LocalStorage { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LocalStorage.RemoveItemAsync(LocalStorageKeys.JwtToken);
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }
}