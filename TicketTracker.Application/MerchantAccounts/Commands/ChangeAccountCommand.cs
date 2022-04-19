using MediatR;
using TicketTracker.Application._Common.Models;

namespace TicketTracker.Application.MerchantAccounts.Commands;

public class ChangeAccountCommand : IRequest<CommandResult>
{
    public Guid MerchantAccountId { get; set; }

    public Guid NewAccountId { get; set; }
}