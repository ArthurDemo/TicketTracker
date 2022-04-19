namespace TicketTracker.Entity.Exceptions;

public class EmailFormatIsIncorrectException : ArgumentNullException
{
    public EmailFormatIsIncorrectException(string parameterName)
        : base(parameterName, "Email 格式不正確")
    {
    }
}