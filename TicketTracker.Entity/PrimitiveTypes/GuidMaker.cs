namespace TicketTracker.Entity.PrimitiveTypes
{
    public class GuidMaker : IDisposable
    {
        [ThreadStatic]
        private static Guid? _injectedGuid;

        private GuidMaker()
        {
        }

        public static Guid NewGuid()
        => _injectedGuid ?? Guid.NewGuid();

        public static IDisposable InjectActualGuid(Guid actualGuid)
        {
            _injectedGuid = actualGuid;
            return new GuidMaker();
        }

        public void Dispose()
        {
            _injectedGuid = null;
        }
    }
}