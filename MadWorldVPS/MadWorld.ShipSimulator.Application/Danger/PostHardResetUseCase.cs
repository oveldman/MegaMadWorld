using MadWorld.ShipSimulator.Domain.Danger;

namespace MadWorld.ShipSimulator.Application.Danger;

public class PostHardResetUseCase
{
    private readonly IDatabaseRepository _repository;

    public PostHardResetUseCase(IDatabaseRepository repository)
    {
        _repository = repository;
    }
    
    public bool PostHardReset()
    {
        return _repository.HardReset();
    }
}