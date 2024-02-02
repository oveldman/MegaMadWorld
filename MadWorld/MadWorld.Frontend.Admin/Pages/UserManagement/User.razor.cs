using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Frontend.Admin.Application.UserManagement;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.Admin.Pages.UserManagement;

public partial class User
{
    [Parameter]
    public string Id { get; set; } = null!;
    
    [Inject]
    public GetUserUseCase GetUseCase { get; set; } = null!;
    
    [Inject]
    public PatchUserUseCase PatchUseCase { get; set; } = null!;
    
    [Inject]
    public IUserService UserService { get; set; } = null!;
    
    private GetRolesResponse _roles = new();
    private UserDetails _user = new();

    protected override async Task OnInitializedAsync()
    {
        _roles = await UserService.GetAllRoles();
        _user = await GetUseCase.GetUser(Id);
        
        await base.OnInitializedAsync();
    }
}