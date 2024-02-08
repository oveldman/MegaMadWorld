using MadWorld.Backend.Identity.Contracts;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class PatchUserUseCase
{
    private readonly IUserService _userService;
    public PatchUserUseCase(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<DefaultResponse> PatchUser(UserDetails user)
    {
        var request = user.ToRequest();
        return await _userService.PatchUser(request);
    }
}