using Blazored.LocalStorage;
using MadWorld.Backend.Identity.Contracts;

namespace MadWorld.Shared.Blazor.Authentications;

public class TokenRefresher : ITokenRefresher
{
    private readonly IIdentityService _identityService;
    private readonly ILocalStorageService _localStorage;

    public TokenRefresher(IIdentityService identityService, ILocalStorageService localStorage)
    {
        _identityService = identityService;
        _localStorage = localStorage;
    }
    
    public async Task Execute()
    {
        var response = await _localStorage.GetItemAsync<JwtLoginResponse>(LocalStorageKeys.JwtToken);

        if (response is null)
        {
            return;
        }

        var refreshResponse = await _identityService.Refresh(new JwtRefreshRequest()
        {
            RefreshToken = response.RefreshToken
        });
        
        await _localStorage.SetItemAsync(LocalStorageKeys.JwtToken, refreshResponse);
    }
}