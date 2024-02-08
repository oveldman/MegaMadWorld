using System.Net.Http.Json;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Shared.Blazor.Common;

namespace MadWorld.Frontend.Admin.Application.ShipSimulator.Reset;

public class ResetServerService : IResetServerService
{
    private const string Endpoint = "Danger";
    
    private readonly HttpClient _client;
    public ResetServerService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.ShipSimulator);
    }
    
    public async Task<bool> Reset()
    {
        var response = await _client.DeleteAsync($"{Endpoint}/HardReset");
        return response.IsSuccessStatusCode;
    }
}