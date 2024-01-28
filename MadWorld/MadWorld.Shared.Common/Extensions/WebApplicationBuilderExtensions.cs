using MadWorld.Shared.Common.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Shared.Common.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddCommon(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}