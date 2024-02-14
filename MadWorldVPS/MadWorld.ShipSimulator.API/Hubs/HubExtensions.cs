using MadWorld.ShipSimulator.Domain.Companies;

namespace MadWorld.ShipSimulator.API.Hubs;

public static class HubExtensions
{
    public static void AddHubs(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();
        builder.Services.AddScoped<ICompanyLiveUpdater, CompanyLiveUpdater>();
    }
}