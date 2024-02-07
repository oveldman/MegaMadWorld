using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.IntegrationTests.Common;

public static class ServiceCollectionExtensions
{
    public static void RemoveHostedService<T>(this IServiceCollection services)
    {
        var descriptor = services.Single(s => s.ImplementationType == typeof(T));
        services.Remove(descriptor);
    }
}