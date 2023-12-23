using System.Security.Claims;
using Blazored.LocalStorage;
using MadWorld.Shared.Blazor.Common;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadWorld.Shared.Blazor.Authentications;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddAuthentication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        
        builder.Services.AddIdentityHttpClient();
    }
    
    private static void AddIdentityHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(ApiTypes.Identity, (serviceProvider, client) =>
        {
            var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
            client.BaseAddress = new Uri(apiUrlsOption.Value.Identity!);
        });
    }
}