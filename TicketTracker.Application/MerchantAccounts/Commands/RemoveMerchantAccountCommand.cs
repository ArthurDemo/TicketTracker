namespace TicketTracker.Application.MerchantAccounts.Commands;

public class RemoveMerchantAccountCommand : IRequest<CommandResult<MerchantAccount>>
{
    public Guid MerchantAccountId { get; set; }
}