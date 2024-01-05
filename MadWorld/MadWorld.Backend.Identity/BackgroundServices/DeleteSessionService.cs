using Cronos;
using MadWorld.Backend.Identity.Application;
using MadWorld.Shared.Infrastructure.BackgroundServices;

namespace MadWorld.Backend.Identity.BackgroundServices;

public class DeleteSessionService : BackgroundService
{
    private const string ExecuteTime = "0 0 3 * * *";

    private readonly IServiceScopeFactory _serviceFactory;
    private readonly ILogger<DeleteSessionService> _logger;

    public DeleteSessionService(IServiceScopeFactory serviceFactory,ILogger<DeleteSessionService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start Service: DeleteSessionService");
        
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var triggerNextTime = MyCronExpression.Parse(ExecuteTime);
                using PeriodicTimer timer = new(triggerNextTime);
                
                await timer.WaitForNextTickAsync(stoppingToken);
                
                _logger.LogInformation("Start delete sessions");  
            
                using var scope = _serviceFactory.CreateScope();
                var useCase = scope.ServiceProvider.GetRequiredService<DeleteSessionsUseCase>();
                
                await useCase.DeleteSessions();
            
                _logger.LogInformation("Delete sessions finished");
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Delete sessions canceled");
        }
    }
}