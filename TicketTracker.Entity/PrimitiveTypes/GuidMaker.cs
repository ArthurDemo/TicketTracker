namespace TicketTracker.Entity.PrimitiveTypes;

public class GuidMaker : IDisposable
{
    private static ThreadLocal<Guid?> _injectedGuid = new(() => Guid.NewGuid());

    private GuidMaker()
    {
    }

    public void Dispose()
    {
        _injectedGuid.Dispose();
    }

    public static Guid NewGuid()
    {
        return _injectedGuid!.Value!.Value;
    }

    public static IDisposable InjectActualGuid(Guid actualGuid)
    {
        _injectedGuid=new ThreadLocal<Guid?>(() => actualGuid);
        return new GuidMaker();
    }
}