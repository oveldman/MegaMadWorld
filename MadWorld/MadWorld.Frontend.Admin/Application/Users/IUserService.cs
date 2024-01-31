using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Frontend.Admin.Application.Users;

public interface IUserService
{
    Task<GetUsersResponse> GetUsers(int page);
}