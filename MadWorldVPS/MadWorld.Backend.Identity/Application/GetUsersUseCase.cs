using MadWorld.Backend.Identity.Application.Mappers;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;

namespace MadWorld.Backend.Identity.Application;

public class GetUsersUseCase
{
    private readonly IUserRepository _repository;

    public GetUsersUseCase(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public GetUsersResponse GetUsers(int page)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(page);

        var totalCount = _repository.CountUsers();
        var users = _repository.GetUsers(page);
        
        return new GetUsersResponse()
        {
            TotalCount = totalCount,
            Users = users.ToDto()
        };
    }
}