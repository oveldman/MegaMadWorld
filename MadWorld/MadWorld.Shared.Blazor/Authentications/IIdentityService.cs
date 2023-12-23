using MadWorld.Shared.Contracts.Identity;

namespace MadWorld.Shared.Blazor.Authentications;

public interface IIdentityService
{
    Task<JwtLoginResponse> Login(JwtLoginRequest request);
}