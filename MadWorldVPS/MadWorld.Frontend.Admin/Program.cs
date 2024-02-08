using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MadWorld.Frontend.Admin;
using MadWorld.Frontend.Admin.Extensions;
using MadWorld.Shared.Blazor.Authentications;
using MadWorld.Shared.Blazor.Common;
using MadWorld.Shared.Blazor.ShipSimulator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddCommon();
builder.AddAuthentication();
builder.AddShipSimulator();
builder.AddApplication();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();