using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class EmailFormatIsIncorrectException : SpecException<EmailFormatIsIncorrectException>
{
    private const string itemName = "Email格式";

    public EmailFormatIsIncorrectException()
    {
    }

    public EmailFormatIsIncorrectException(string parameterName)
        : base(itemName, parameterName)
    {
    }
}