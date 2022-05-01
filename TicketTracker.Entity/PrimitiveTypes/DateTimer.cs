namespace TicketTracker.Entity.PrimitiveTypes;

public class DateTimer : IDisposable
{
    private static ThreadLocal<DateTime?>? _injectedDateTime = new(() => DateTime.Now);

    private DateTimer()
    {
    }

    public static DateTime Now => _injectedDateTime!.Value!.Value;

    public void Dispose()
    {
        _injectedDateTime?.Dispose();
    }

    public static IDisposable InjectActualDateTime(DateTime actualDateTime)
    {
        _injectedDateTime = new ThreadLocal<DateTime?>(() => actualDateTime);

        return new DateTimer();
    }
}