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
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddScoped<MyHttpMessageHandler>();
        builder.Services.AddSingleton<MyAccessTokenProvider>();
        builder.Services.AddScoped<ITokenRefresher, TokenRefresher>();
        builder.Services.AddSingleton<IAccessTokenProvider>(provider => provider.GetService<MyAccessTokenProvider>()!);
        builder.Services.AddSingleton<IAccessTokenWriter>(provider => provider.GetService<MyAccessTokenProvider>()!);
        
        builder.Services.AddIdentityHttpClients();
    }
    
    private static void AddIdentityHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient(ApiTypes.Identity, (serviceProvider, client) =>
        {
            var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
            client.BaseAddress = new Uri(apiUrlsOption.Value.Identity!);
        }).AddHttpMessageHandler<MyHttpMessageHandler>();

        services.AddHttpClient(ApiTypes.IdentityAnonymous, (serviceProvider, client) =>
        {
            var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
            client.BaseAddress = new Uri(apiUrlsOption.Value.Identity!);
        });
    }
}