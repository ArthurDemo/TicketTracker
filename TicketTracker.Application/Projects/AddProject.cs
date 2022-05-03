using TicketTracker.Application._Common;
using TicketTracker.Application.Projects.Commands;

namespace TicketTracker.Application.Projects;

public class AddProject : IRequestHandler<AddProjectCommand, CommandResult<Project>>
{
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;

    public AddProject(IProjectRepository projectRepository, IMediator mediator)
    {
        _projectRepository = projectRepository;
        _mediator = mediator;
    }

    public async Task<CommandResult<Project>> Handle(AddProjectCommand command, CancellationToken cancellationToken)
    {
        var project = Project.Create(
            command.ProjectName,
            new ProjectManager(command.ProjectManagerAccountIds!.Select(actId => new AccountId(actId)).ToList()));

        _projectRepository.Add(project);

        await _mediator.Publish<AddedProject>(
            new AddedProject(command.MerchantAccountId, command.WorkSpaceName, project.Id),
            cancellationToken);

        return new CommandResult<Project>(project);
    }
}