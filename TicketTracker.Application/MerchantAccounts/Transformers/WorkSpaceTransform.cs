using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.MerchantAccounts.Transformers;

public static class WorkSpaceTransform
{
    public static WorkSpace ExtractWorkSpaceParameter(WorkSpaceParameter parameter)
    {
        var workSpace = WorkSpace.Create(
            parameter.Name,
            parameter.ProjectIds!.Select(prj => new ProjectId(prj)).ToList(),
            parameter.UpperLimit);
        return workSpace;
    }
}