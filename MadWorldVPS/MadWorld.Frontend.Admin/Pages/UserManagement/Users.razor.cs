using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Frontend.Admin.Application.UserManagement;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadWorld.Frontend.Admin.Pages.UserManagement;

public partial class Users
{
    [Inject]
    public IUserService UserService { get; set; } = null!;
    
    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    private GetUsersResponse _response = new();

    private int _currentPage = 0;

    protected override async Task OnInitializedAsync()
    {
        await LoadUsers();
        
        await base.OnInitializedAsync();
    }
    
    private Task LoadData() => Task.CompletedTask;

    private async Task OnPage(PagerEventArgs args)
    {
        if (_currentPage != args.PageIndex)
        {
            _currentPage = args.PageIndex;
            await LoadUsers();   
        }
    }

    private void OpenUser(DataGridRowMouseEventArgs<UserContract> user)
    {
        Navigation.NavigateTo($"/User/{user.Data.Id}");
    }

    private async Task LoadUsers()
    {
        _response = await UserService.GetUsers(_currentPage);
    }
}