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
        var jwtResponse = await TryGetJwtResponse();
        var identity = CreateIdentity(jwtResponse);
        return CreateState(identity);
    }
    
    private async Task<JwtLoginResponse?> TryGetJwtResponse()
    {
        JwtLoginResponse? jwtResponse = null;
        if (await _localStorageService.ContainKeyAsync(LocalStorageKeys.JwtToken))
        {
            jwtResponse = await _localStorageService.GetItemAsync<JwtLoginResponse>(LocalStorageKeys.JwtToken);            
        }

        return jwtResponse;
    }

    private ClaimsIdentity CreateIdentity(JwtLoginResponse? jwtResponse)
    {
        var identity = new ClaimsIdentity();
        
        if (jwtResponse is not null)
        {
            identity = TryGetIdentityFromToken(jwtResponse);
        }
        else
        {
            _accessTokenWriter.RemoveToken();
        }

        return identity;
    }

    private ClaimsIdentity TryGetIdentityFromToken(JwtLoginResponse jwtResponse)
    {
        ClaimsIdentity identity;
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

        return identity;
    }
    
    private AuthenticationState CreateState(ClaimsIdentity identity)
    {
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