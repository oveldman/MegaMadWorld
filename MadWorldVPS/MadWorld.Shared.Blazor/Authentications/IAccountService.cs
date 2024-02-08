using MadWorld.Backend.Identity.Contracts.UserInfo;

namespace MadWorld.Shared.Blazor.Authentications;

public interface IAccountService
{
    Task<InfoResponse> GetInfo();
}