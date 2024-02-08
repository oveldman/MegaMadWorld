namespace MadWorld.Frontend.Admin.Application.ShipSimulator.Reset;

public interface IResetServerService
{
    Task<bool> Reset();
}