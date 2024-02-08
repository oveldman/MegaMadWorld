using Cronos;

namespace MadWorld.Shared.Infrastructure.BackgroundServices;

public static class MyCronExpression
{
    public static TimeSpan Parse(string executeTime)
    {
        var expression = CronExpression.Parse(executeTime);
        DateTime? nextUtc = expression.GetNextOccurrence(DateTime.UtcNow) ?? throw new InvalidOperationException();
        return TimeSpan.FromTicks((nextUtc.Value - DateTime.UtcNow).Ticks); 
    }
}