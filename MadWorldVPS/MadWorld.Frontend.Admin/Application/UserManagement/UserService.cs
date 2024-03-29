using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class UserService : IUserService
{
    private const string Endpoint = "UserManager";
    
    private readonly HttpClient _client;
    public UserService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.Identity);
    }
    
    public async Task<DefaultResponse> DeleteSessions(string userId)
    {
        var response = await _client.DeleteAsync($"{Endpoint}/Sessions?userId={userId}");

        if (!response.IsSuccessStatusCode)
        {
            return new DefaultResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<DefaultResponse>() ?? new DefaultResponse();
    }
    
    public async Task<GetRolesResponse> GetAllRoles()
    {
        var response = await _client.GetAsync($"{Endpoint}/Roles");

        if (!response.IsSuccessStatusCode)
        {
            return new GetRolesResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<GetRolesResponse>() ?? new GetRolesResponse();
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
    
    public async Task<GetUserResponse> GetUser(string id)
    {
        var response = await _client.GetAsync($"{Endpoint}/User?id={id}");

        if (!response.IsSuccessStatusCode)
        {
            return GetUserResponse.Empty;
        }
        
        return await response.Content.ReadFromJsonAsync<GetUserResponse>() ?? GetUserResponse.Empty;
    }
    
    public async Task<DefaultResponse> PatchUser(PatchUserRequest request)
    {
        var response = await _client.PatchAsJsonAsync($"{Endpoint}/User", request);

        if (!response.IsSuccessStatusCode)
        {
            return new DefaultResponse();
        }
        
        return await response.Content.ReadFromJsonAsync<DefaultResponse>() ?? new DefaultResponse();
    }
}