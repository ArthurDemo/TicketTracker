using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class WorkSpaceCouldNotFoundException : ObjectNullException<WorkSpaceCouldNotFoundException>
{
    private const string entityName = "工作域";

    public WorkSpaceCouldNotFoundException()
    {
    }

    public WorkSpaceCouldNotFoundException(string parameterName)
        : base(entityName, parameterName)
    {
    }
}