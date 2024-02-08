using MadWorld.Backend.Identity.Infrastructure;

namespace MadWorld.Backend.Identity.Application;

public sealed class DeleteExpiredSessionsUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteExpiredSessionsUseCase> _logger;

    public DeleteExpiredSessionsUseCase(IUserRepository userRepository, ILogger<DeleteExpiredSessionsUseCase> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task DeleteExpiredSessions()
    {
        var deletedRows = await _userRepository.DeleteExpiredRefreshTokens();
        
        _logger.LogInformation("Sessions deleted: {Amount}", deletedRows);
    }
}