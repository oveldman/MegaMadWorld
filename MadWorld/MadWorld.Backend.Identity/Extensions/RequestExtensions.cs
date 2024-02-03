using Microsoft.Extensions.Primitives;

namespace MadWorld.Backend.Identity.Extensions;

public static class RequestExtensions
{
    public static string GetOriginUrl(this HttpRequest request)
    {
        request.Headers.TryGetValue("Origin", out var originValues);
        var url = originValues.Count != 0 ? originValues[0]! : string.Empty;
        return url.TrimEnd('/') + "/";
    }
}