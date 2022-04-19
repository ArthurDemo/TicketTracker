namespace TicketTracker.Entity.Exceptions;

public class WorkSpaceCouldNotFoundException : ArgumentNullException
{
    public WorkSpaceCouldNotFoundException(string parameterName)
        : base(parameterName, "找不到指定的工作域或不存在")
    {
    }
}