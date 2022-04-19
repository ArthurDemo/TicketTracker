using MediatR;
using TicketTracker.Application._Common;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.Projects.Commands;
using TicketTracker.Entity;
using TicketTracker.Entity.DomainEvents;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.Projects
{
    public class AddProject : IRequestHandler<AddProjectCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;

        public AddProject(IProjectRepository projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(AddProjectCommand command, CancellationToken cancellationToken)
        {
            var project = Project.Create(
                command.ProjectName,
                new ProjectManager(command.ProjectManagerAccountIds.Select(actId => new AccountId(actId)).ToList()));

            _projectRepository.Add(project);

            await _mediator.Publish<AddedProject>(
                new AddedProject(command.MerchantAccountId, command.WorkSpaceName, project.Id),
                cancellationToken);

            return new CommandResult();
        }
    }
}