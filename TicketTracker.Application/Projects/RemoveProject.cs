using TicketTracker.Application._Common;
using TicketTracker.Application.Projects.Commands;

namespace TicketTracker.Application.Projects;

public class RemoveProject : IRequestHandler<RemoveProjectCommand, CommandResult>
{
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;

    public RemoveProject(IProjectRepository projectRepository, IMediator mediator)
    {
        _projectRepository = projectRepository;
        _mediator = mediator;
    }

    public async Task<CommandResult> Handle(RemoveProjectCommand command, CancellationToken cancellationToken)
    {
        _projectRepository.DeleteById(new ProjectId(command.ProjectId));

        await _mediator.Publish<RemovedProject>(
            new RemovedProject(command.MerchantAccountId, command.WorkSpaceName, new ProjectId(command.ProjectId)),
            cancellationToken);

        return new CommandResult();
    }
}