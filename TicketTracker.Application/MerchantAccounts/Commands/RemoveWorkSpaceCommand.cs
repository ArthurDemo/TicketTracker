using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Transformers;

namespace TicketTracker.Application.MerchantAccounts.Commands;

public class RemoveWorkSpaceCommand : IRequest<CommandResult>, IWorkSpaceParameter
{
    public Guid MerchantAccountId { get; set; }

    public (string name, IEnumerable<Guid> projectIds, uint upperLimit) WorkSpace { get; set; }
}