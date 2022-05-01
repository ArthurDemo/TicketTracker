using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class MerchantAccountCouldNotFoundException : ObjectNullException<MerchantAccountCouldNotFoundException>
{
    private const string entityName = "商戶帳號";

    public MerchantAccountCouldNotFoundException()
    {
    }

    public MerchantAccountCouldNotFoundException(string parameterName)
        : base(entityName, parameterName)
    {
    }
}