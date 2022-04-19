namespace TicketTracker.Entity.Exceptions;

public class MerchantAccountCouldNotFoundException : ArgumentNullException
{
    public MerchantAccountCouldNotFoundException(string parameterName)
        : base(parameterName, "商戶帳號無法使用或不存在")
    {
    }
}