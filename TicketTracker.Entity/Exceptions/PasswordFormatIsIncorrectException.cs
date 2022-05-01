using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class PasswordFormatIsIncorrectException : SpecException<PasswordFormatIsIncorrectException>
{
    private const string itemName = "密碼";

    public PasswordFormatIsIncorrectException()
    {
    }

    public PasswordFormatIsIncorrectException(string parameterName)
        : base(itemName, parameterName)
    {
    }
}