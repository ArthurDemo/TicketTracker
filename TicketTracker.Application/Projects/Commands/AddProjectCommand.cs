namespace TicketTracker.Application.Projects.Commands;

public class AddProjectCommand : IRequest<CommandResult<Project>>
{
    public Guid MerchantAccountId { get; init; }

    public string WorkSpaceName { get; init; } = null!;

    public string ProjectName { get; init; } = null!;

    public IEnumerable<Guid>? ProjectManagerAccountIds { get; init; }
}