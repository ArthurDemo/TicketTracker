using TicketTracker.Application.MerchantAccounts.Transformers;

namespace TicketTracker.Application.MerchantAccounts.Commands;

public record RegisterMerchantAccountCmd : IRequest<CommandResult>
{
    public Guid AccountId { get; set; }

    public IEnumerable<IWorkSpaceParameter>? WorkSpaces { get; set; }
}