using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public interface IUserService
{
    Task<GetRolesResponse> GetAllRoles();
    Task<GetUsersResponse> GetUsers(int page);
    Task<GetUserResponse> GetUser(string id);
}