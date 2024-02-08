using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserInfo;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Shared.Blazor.Authentications;

public class IdentityService : IIdentityService
{
    private const string Endpoint = "Account";
    
    private readonly HttpClient _client;
    public IdentityService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.IdentityAnonymous);
    }
    
    public async Task<JwtLoginResponse> Login(JwtLoginRequest request)
    {
        var response = await _client.PostAsJsonAsync($"{Endpoint}/JwtLogin", request);

        if (!response.IsSuccessStatusCode)
        {
            return new JwtLoginResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<JwtLoginResponse>() ?? new JwtLoginResponse();
    }
    
    public async Task<JwtRefreshResponse> Refresh(JwtRefreshRequest request)
    {
        var response = await _client.PostAsJsonAsync($"{Endpoint}/JwtRefresh", request);

        if (!response.IsSuccessStatusCode)
        {
            return new JwtRefreshResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<JwtRefreshResponse>() ?? new JwtRefreshResponse();
    }
}