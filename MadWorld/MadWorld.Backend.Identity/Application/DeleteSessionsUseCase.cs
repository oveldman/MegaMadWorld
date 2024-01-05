using MadWorld.Backend.Identity.Infrastructure;

namespace MadWorld.Backend.Identity.Application;

public sealed class DeleteSessionsUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteSessionsUseCase> _logger;

    public DeleteSessionsUseCase(IUserRepository userRepository, ILogger<DeleteSessionsUseCase> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task DeleteSessions()
    {
        var deletedRows = await _userRepository.DeleteExpiredRefreshTokens();
        
        _logger.LogInformation("Sessions deleted: {Amount}", deletedRows);
    }
}