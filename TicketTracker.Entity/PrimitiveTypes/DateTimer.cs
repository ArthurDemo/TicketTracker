namespace TicketTracker.Entity.PrimitiveTypes
{
    public class DateTimer : IDisposable
    {
        [ThreadStatic]
        private static DateTime? _injectedDateTime;

        private DateTimer()
        {
        }

        public static DateTime Now
        {
            get
            {
                return _injectedDateTime ?? DateTime.Now;
            }
        }

        public static IDisposable InjectActualDateTime(DateTime actualDateTime)
        {
            _injectedDateTime = actualDateTime;

            return new DateTimer();
        }

        public void Dispose()
        {
            _injectedDateTime = null;
        }
    }
}