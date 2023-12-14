using MadWorld.ShipSimulator.Application.Danger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.ShipSimulator.Application.CommonLogic.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<PostHardResetUseCase>();
    }
}