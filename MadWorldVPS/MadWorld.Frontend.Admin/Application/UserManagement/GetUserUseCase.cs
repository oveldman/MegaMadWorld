namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class GetUserUseCase
{
    private readonly IUserService _userService;
    public GetUserUseCase(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDetails> GetUser(string id)
    {
        var roles = await _userService.GetAllRoles();
        var user = await _userService.GetUser(id);
        return user.ToDetails(roles.Roles);
    }
}