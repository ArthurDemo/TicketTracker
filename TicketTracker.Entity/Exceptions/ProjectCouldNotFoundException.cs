namespace TicketTracker.Entity.Exceptions;

public class ProjectCouldNotFoundException : ArgumentNullException
{
    public ProjectCouldNotFoundException(string parameterName)
        : base(parameterName, "找不到指定的專案或不存在")
    {
    }
}