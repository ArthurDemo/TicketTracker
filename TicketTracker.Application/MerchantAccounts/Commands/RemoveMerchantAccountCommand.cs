using MediatR;
using TicketTracker.Application._Common.Models;

namespace TicketTracker.Application.MerchantAccounts.Commands;

public class RemoveMerchantAccountCommand : IRequest<CommandResult>
{
    public Guid MerchantAccountId { get; set; }
}