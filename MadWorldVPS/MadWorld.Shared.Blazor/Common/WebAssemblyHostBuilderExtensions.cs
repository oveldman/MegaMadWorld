using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace MadWorld.Shared.Blazor.Common;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCommon(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddRadzenComponents();
        
        builder.Services.AddConfigurationSettings(builder.Configuration);
    }
    
    private static void AddConfigurationSettings(this IServiceCollection services, WebAssemblyHostConfiguration configuration)
    {
        services
            .AddOptions<ApiUrls>()
            .Configure(configuration.GetSection(ApiUrls.SectionName).Bind);
    }
}