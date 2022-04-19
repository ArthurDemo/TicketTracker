using MediatR;
using TicketTracker.Application._Common;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.Projects.Commands;
using TicketTracker.Entity;
using TicketTracker.Entity.DomainEvents;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.Projects
{
    public class RemoveProject : IRequestHandler<RemoveProjectCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;

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
}