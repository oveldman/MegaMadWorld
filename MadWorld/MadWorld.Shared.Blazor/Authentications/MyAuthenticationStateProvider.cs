using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Shared.Blazor.Authentications;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public MyAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.JwtToken);
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                token = token.Trim();
                identity = RetrieveUserFromJwt(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                identity = new ClaimsIdentity();
            }
        }
        
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private static ClaimsIdentity RetrieveUserFromJwt(string jwt)
    {
        var claims = ParseClaimsFromJwt(jwt).ToList();
        return new ClaimsIdentity(claims, "JWT");
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}