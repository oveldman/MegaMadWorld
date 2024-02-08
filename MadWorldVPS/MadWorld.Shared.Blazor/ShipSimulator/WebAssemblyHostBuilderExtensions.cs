using MadWorld.Shared.Blazor.Authentications;
using MadWorld.Shared.Blazor.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadWorld.Shared.Blazor.ShipSimulator;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddShipSimulator(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddShipSimulatorHttpClients();
    }
    
    private static void AddShipSimulatorHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient(ApiTypes.ShipSimulator, (serviceProvider, client) =>
        {
            var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
            client.BaseAddress = new Uri(apiUrlsOption.Value.ShipSimulator!);
        }).AddHttpMessageHandler<MyHttpMessageHandler>();
    }
}