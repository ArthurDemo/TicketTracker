using MediatR;
using TicketTracker.Application._Common.Models;

namespace TicketTracker.Application.Projects.Commands;

public class RemoveProjectCommand : IRequest<CommandResult>
{
    public Guid MerchantAccountId { get; set; }

    public string WorkSpaceName { get; set; } = null!;

    public Guid ProjectId { get; set; }
}