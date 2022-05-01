namespace TicketTracker.Application.MerchantAccounts.Transformers;

public interface IWorkSpaceParameter
{
    (string name, IEnumerable<Guid> projectIds, uint upperLimit) WorkSpace { get; set; }
}

public static class WorkSpaceTransform
{
    public static WorkSpace ExtractWorkSpaceParameter(IWorkSpaceParameter parameter)
    {
        var workSpace = WorkSpace.Create(
            parameter.WorkSpace.name,
            parameter.WorkSpace.projectIds!.Select(prj => new ProjectId(prj)).ToList(),
            parameter.WorkSpace.upperLimit);
        return workSpace;
    }
}