using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class EmailFormatIsIncorrectException : SpecException<EmailFormatIsIncorrectException>
{
    private const string itemName = "Email格式";
    private const string failReason = "Email資料不得為空，或是Email格式不正確";

    public EmailFormatIsIncorrectException()
    {
    }

    public EmailFormatIsIncorrectException(string parameterName)
        : base(itemName, failReason, parameterName)
    {
    }
}