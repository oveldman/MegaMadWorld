using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Frontend.Admin.Application.Users;

public class UserService : IUserService
{
    private const string Endpoint = "UserManager";
    
    private readonly HttpClient _client;
    public UserService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.Identity);
    }

    public async Task<GetUsersResponse> GetUsers(int page)
    {
        var response = await _client.GetAsync($"{Endpoint}/Users?page={page}");

        if (!response.IsSuccessStatusCode)
        {
            return new GetUsersResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<GetUsersResponse>() ?? new GetUsersResponse();
    }
}