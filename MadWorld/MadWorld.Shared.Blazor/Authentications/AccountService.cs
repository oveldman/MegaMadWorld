using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts.UserInfo;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Shared.Blazor.Authentications;

public class AccountService : IAccountService
{
    private const string Endpoint = "Account";
    
    private readonly HttpClient _client;
    public AccountService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.Identity);
    }
    
    public async Task<InfoResponse> GetInfo()
    {
        var response = await _client.GetAsync($"{Endpoint}/manage/info");

        if (!response.IsSuccessStatusCode)
        {
            return new InfoResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<InfoResponse>() ?? new InfoResponse();
    }
}