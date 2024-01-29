using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.CommonExceptions;
using MadWorld.Backend.Identity.Infrastructure;

namespace MadWorld.Backend.Identity.Application;

public class DeleteSessionUseCase
{
    private readonly UserRepository _repository;

    public DeleteSessionUseCase(UserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<DefaultResponse> DeleteSession(DeleteSessionRequest request)
    {
        if (string.IsNullOrEmpty(request.UserId))
        {
            throw new FieldRequiredException(nameof(request.UserId));
        }
        
        await _repository.DeleteUserSessions(request.UserId);
        
        return new DefaultResponse();
    }
}