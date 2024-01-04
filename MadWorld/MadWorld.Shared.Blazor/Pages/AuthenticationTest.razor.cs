using MadWorld.Backend.Identity.Contracts.UserInfo;
using MadWorld.Shared.Blazor.Authentications;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Shared.Blazor.Pages;

public partial class AuthenticationTest
{
    [Inject]
    public IAccountService AccountService { get; set; } = null!;
    
    public InfoResponse UserInfo { get; set; } = new();

    public async Task GetUserInfo()
    {
        UserInfo = await AccountService.GetInfo();
    }
}