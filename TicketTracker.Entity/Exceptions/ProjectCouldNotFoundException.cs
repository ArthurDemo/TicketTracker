using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Exceptions;

public class ProjectCouldNotFoundException : ObjectNullException<ProjectCouldNotFoundException>
{
    private const string entityName = "專案";

    public ProjectCouldNotFoundException()
    {
    }

    public ProjectCouldNotFoundException(string parameterName)
        : base(entityName, parameterName)
    {
    }
}