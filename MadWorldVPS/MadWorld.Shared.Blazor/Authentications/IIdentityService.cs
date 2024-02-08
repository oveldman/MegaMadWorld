using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserInfo;

namespace MadWorld.Shared.Blazor.Authentications;

public interface IIdentityService
{
    Task<JwtLoginResponse> Login(JwtLoginRequest request);
    Task<JwtRefreshResponse> Refresh(JwtRefreshRequest request);
}