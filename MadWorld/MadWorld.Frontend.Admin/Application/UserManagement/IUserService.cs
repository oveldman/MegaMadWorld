using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public interface IUserService
{
    Task<DefaultResponse> DeleteSessions(string userId);
    Task<GetRolesResponse> GetAllRoles();
    Task<GetUsersResponse> GetUsers(int page);
    Task<GetUserResponse> GetUser(string id);
    Task<DefaultResponse> PatchUser(PatchUserRequest request);
}