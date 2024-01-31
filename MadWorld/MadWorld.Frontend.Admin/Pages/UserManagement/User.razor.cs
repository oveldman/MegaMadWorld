using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.Admin.Pages.UserManagement;

public partial class User
{
    [Parameter]
    public string Id { get; set; } = null!;
}