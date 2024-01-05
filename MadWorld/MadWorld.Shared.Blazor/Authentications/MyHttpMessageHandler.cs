using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MadWorld.Shared.Blazor.Authentications;

public class MyHttpMessageHandler : DelegatingHandler
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigation;
    private readonly IAccessTokenProvider _provider;
    private readonly ITokenRefresher _tokenRefresher;

    public MyHttpMessageHandler(
        AuthenticationStateProvider authenticationStateProvider,
        NavigationManager navigation, 
        IAccessTokenProvider provider, 
        ITokenRefresher tokenRefresher)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _navigation = navigation;
        _provider = provider;
        _tokenRefresher = tokenRefresher;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            await AddAuthorizationHeader(request);
            return await base.SendAsync(request, cancellationToken);
        }
        catch (RefreshTokenInvalidException)
        {
            return CreateExceptionMessage();
        }
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            AddAuthorizationHeader(request).Wait(cancellationToken);
            return base.Send(request, cancellationToken);
        }
        catch (RefreshTokenInvalidException)
        {
            return CreateExceptionMessage();
        }
    }

    private static HttpResponseMessage CreateExceptionMessage()
    {
        return new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Unauthorized,
        };
    }
    
    private async Task AddAuthorizationHeader(HttpRequestMessage request)
    {
        var accessToken = await _provider.RequestAccessToken();
        if (accessToken.TryGetToken(out var token))
        {
            token = await TryRefreshToken(token);

            if (token is null)
            {
                return;
            }
            
            AddBearerToken(request, token);  
        }
    }

    private async Task<AccessToken?> TryRefreshToken(AccessToken token)
    {
        if (token.Expires <= DateTimeOffset.UtcNow.AddMinutes(-5)) return token;

        await _tokenRefresher.Execute();
        await _authenticationStateProvider.GetAuthenticationStateAsync();
            
        var accessToken = await _provider.RequestAccessToken();

        if (accessToken.Status == AccessTokenResultStatus.RequiresRedirect)
        {
            _navigation.NavigateTo("/Login");
            throw new RefreshTokenInvalidException();
        }
        
        return accessToken.TryGetToken(out var newToken) ? newToken : null;
    }
    
    private static void AddBearerToken(HttpRequestMessage request, AccessToken token)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
    }
}