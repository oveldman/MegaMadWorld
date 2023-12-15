using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Backend.Identity.Application;

public class GetUserUseCase
{
    public GetUserResponse GetUser(string id)
    {
        return new GetUserResponse();
    }
}