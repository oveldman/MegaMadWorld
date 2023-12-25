using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Shared.Blazor.Authentications;

public class IdentityService : IIdentityService
{
    private const string Endpoint = "Account";
    
    private readonly HttpClient _client;
    public IdentityService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.Identity);
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
}