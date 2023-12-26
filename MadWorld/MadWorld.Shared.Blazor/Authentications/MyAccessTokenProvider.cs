using MadWorld.Shared.Blazor.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace MadWorld.Shared.Blazor.Authentications;

public class MyAccessTokenProvider : IAccessTokenProvider, IAccessTokenWriter
{
    private AccessToken? _token = new();
    private readonly IOptions<ApiUrls> _urls;
    private string RefreshUrl => $"{_urls.Value.Identity}/Account/RefreshToken";

    public MyAccessTokenProvider(IOptions<ApiUrls> urls)
    {
        _urls = urls;
    }
    public ValueTask<AccessTokenResult> RequestAccessToken()
    {
        var options = new AccessTokenRequestOptions()
        {
            ReturnUrl = "/Login"
        };
        
        return RequestAccessToken(options);
    }

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
    {
        var interactiveRequestOptions = new InteractiveRequestOptions()
        {
            Interaction = InteractionType.GetToken,
            ReturnUrl = options.ReturnUrl!,
        };
        
        return ValueTask.FromResult(
            new AccessTokenResult(AccessTokenResultStatus.Success, _token!, RefreshUrl, interactiveRequestOptions));
    }

    public void SetAccessToken(string token, DateTimeOffset expires)
    {
        _token = new AccessToken()
        {
            GrantedScopes = Array.Empty<string>(),
            Expires = expires,
            Value = token
        };
    }

    public void RemoveToken()
    {
        _token = new AccessToken();
    }
}