using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Shared.Blazor.Authentications;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddAuthentication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();

        builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();
        builder.Services.AddCascadingAuthenticationState();
    }
}