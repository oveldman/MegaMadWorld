using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace MadWorld.Shared.Blazor.Common;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCommon(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddRadzenComponents();
    }
}