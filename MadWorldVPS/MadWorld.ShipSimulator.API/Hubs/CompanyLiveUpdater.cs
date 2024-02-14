using MadWorld.ShipSimulator.Domain.Companies;
using Microsoft.AspNetCore.SignalR;

namespace MadWorld.ShipSimulator.API.Hubs;

public class CompanyLiveUpdater : ICompanyLiveUpdater
{
    private readonly IHubContext<CompanyHub> _hubContext;

    public CompanyLiveUpdater(IHubContext<CompanyHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task UpdateCompanyAsync(Company company)
    {
        await _hubContext.Clients.User(company.UserId.ToString()).SendAsync("CompanyUpdated", company);
    }
}