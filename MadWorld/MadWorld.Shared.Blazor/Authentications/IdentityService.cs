using System.Net.Http.Json;
using MadWorld.Shared.Blazor.Common;
using MadWorld.Shared.Contracts.Identity;

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
        return await response.Content.ReadFromJsonAsync<JwtLoginResponse>() ?? new JwtLoginResponse();
    }
}