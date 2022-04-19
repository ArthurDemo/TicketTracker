using MediatR;
using TicketTracker.Application._Common.Models;

namespace TicketTracker.Application.Projects.Commands;

public class AddProjectCommand : IRequest<CommandResult>
{
    public Guid MerchantAccountId { get; set; }

    public string WorkSpaceName { get; set; }

    public string ProjectName { get; set; }

    public IEnumerable<Guid> ProjectManagerAccountIds { get; set; }
}