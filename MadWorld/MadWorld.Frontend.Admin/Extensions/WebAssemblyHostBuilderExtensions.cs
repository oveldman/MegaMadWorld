using MadWorld.Frontend.Admin.Application.Users;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MadWorld.Frontend.Admin.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddApplication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
    }
}