namespace TicketTracker.Application._Common.Specifications;

public class WorkSpaceSpec : Specification<WorkSpace>
{
    public override Expression<Func<WorkSpace, bool>> AsExpression()
    {
        return ws =>
            string.IsNullOrWhiteSpace(ws.Name) &&
            ws.ProjectUpperLimited > 0 &&
            ws.Projects != null && ws.Projects.Any();
    }
}