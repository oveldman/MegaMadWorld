using MadWorld.Backend.Identity.Application.Mappers;
using MadWorld.Backend.Identity.Endpoints;
using MadWorld.Backend.Identity.Infrastructure;

namespace MadWorld.Backend.Identity.Application;

public class GetUsersUseCase
{
    private readonly IUserRepository _repository;

    public GetUsersUseCase(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public GetUsersResponse GetUsers()
    {
        var users = _repository.GetUsers();
        return new GetUsersResponse()
        {
            Users = users.ToDto()
        };
    }
}