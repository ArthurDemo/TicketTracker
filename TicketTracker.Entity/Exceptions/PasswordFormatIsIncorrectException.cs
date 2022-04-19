namespace TicketTracker.Entity.Exceptions;

public class PasswordFormatIsIncorrectException : ArgumentNullException
{
    public PasswordFormatIsIncorrectException(string parameterName)
        : base(parameterName, "密碼不得為空")
    { }
}