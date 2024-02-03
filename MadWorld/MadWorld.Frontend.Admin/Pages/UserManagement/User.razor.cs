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
    public NavigationManager Navigation { get; set; } = null!;
    
    [Inject]
    public IUserService UserService { get; set; } = null!;
    
    private UserDetails _user = new();
    
    private const string DefaultErrorMessage = "Something went wrong. Please try again later.";
    private string DefaultPatchSuccessMessage => $"The user '{_user.Email}' has been updated.";
    private string DefaultDeleteSessionsMessage => $"The sessions of the user '{_user.Email}' have been deleted.";
    
    private bool ShowError;
    private bool ShowDeleteSessionsSuccess;
    private bool ShowPatchSuccess;

    protected override async Task OnInitializedAsync()
    {
        _user = await GetUseCase.GetUser(Id);
        
        await base.OnInitializedAsync();
    }
    
    private async Task Save()
    {
        ResetMessages();
        var response = await PatchUseCase.PatchUser(_user);
        _user = await GetUseCase.GetUser(Id);
        ShowPatchSuccess = response.IsSuccess;
        ShowError = !response.IsSuccess;
    }
    
    private void Cancel()
    {
        Navigation.NavigateTo("/Users");
    }
    
    private async Task DeleteSessions()
    {
        ResetMessages();
        var response = await UserService.DeleteSessions(_user.Id);
        _user = await GetUseCase.GetUser(Id);
        ShowDeleteSessionsSuccess = response.IsSuccess;
        ShowError = !response.IsSuccess;
    }
    
    private void ResetMessages()
    {
        ShowDeleteSessionsSuccess = false;
        ShowPatchSuccess = false;
        ShowError = false;
    }
}