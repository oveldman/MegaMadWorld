using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Frontend.Admin.Application.UserManagement;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.Admin.Pages.UserManagement;

public partial class User
{
    [Parameter]
    public string Id { get; set; } = null!;
    
    [Inject]
    public IUserService UserService { get; set; } = null!;
    
    private GetUserResponse UserResponse { get; set; } = GetUserResponse.Empty;

    protected override async Task OnInitializedAsync()
    {
        UserResponse = await UserService.GetUser(Id);
        
        await base.OnInitializedAsync();
    }
}