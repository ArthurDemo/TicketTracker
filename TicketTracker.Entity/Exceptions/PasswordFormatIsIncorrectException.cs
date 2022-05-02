using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class PasswordFormatIsIncorrectException : SpecException<PasswordFormatIsIncorrectException>
{
    private const string itemName = "密碼";
    private const string failReason = "密碼資料不得為空，或密碼格式不正確";

    public PasswordFormatIsIncorrectException()
    {
    }

    public PasswordFormatIsIncorrectException(string parameterName)
        : base(itemName, failReason, parameterName)
    {
    }
}