using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using MadWorld.Backend.Identity.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Shared.Blazor.Authentications;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IAccessTokenWriter _accessTokenWriter;

    public MyAuthenticationStateProvider(ILocalStorageService localStorageService, IAccessTokenWriter accessTokenWriter)
    {
        _localStorageService = localStorageService;
        _accessTokenWriter = accessTokenWriter;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        JwtLoginResponse? jwtResponse = null;
        if (await _localStorageService.ContainKeyAsync(LocalStorageKeys.JwtToken))
        {
            jwtResponse = await _localStorageService.GetItemAsync<JwtLoginResponse>(LocalStorageKeys.JwtToken);            
        }

        if (jwtResponse is not null)
        {
            try
            {
                identity = RetrieveUserFromJwt(jwtResponse.Jwt);
                _accessTokenWriter.SetAccessToken(jwtResponse.Jwt, jwtResponse.Expires);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                identity = new ClaimsIdentity();
                _accessTokenWriter.RemoveToken();
            }
        }
        else
        {
            _accessTokenWriter.RemoveToken();
        }
        
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private static ClaimsIdentity RetrieveUserFromJwt(string jwt)
    {
        var claims = ParseClaimsFromJwt(jwt).ToList();
        return new ClaimsIdentity(claims, "jwt", "nameid", "role");
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}