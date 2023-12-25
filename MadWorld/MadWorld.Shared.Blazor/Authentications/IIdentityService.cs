using MadWorld.Backend.Identity.Contracts;

namespace MadWorld.Shared.Blazor.Authentications;

public interface IIdentityService
{
    Task<JwtLoginResponse> Login(JwtLoginRequest request);
}