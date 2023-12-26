using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MadWorld.Shared.Blazor.Authentications;

public class MyHttpMessageHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _provider;

    public MyHttpMessageHandler(IAccessTokenProvider provider)
    {
        _provider = provider;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await AddAuthorizationHeader(request);
        return await base.SendAsync(request, cancellationToken);
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AddAuthorizationHeader(request).Wait(cancellationToken);
        return base.Send(request, cancellationToken);
    }
    
    private async Task AddAuthorizationHeader(HttpRequestMessage request)
    {
        var accessToken = await _provider.RequestAccessToken();
        if (accessToken.TryGetToken(out var token) && token.Expires > DateTimeOffset.UtcNow)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        }
    }
}