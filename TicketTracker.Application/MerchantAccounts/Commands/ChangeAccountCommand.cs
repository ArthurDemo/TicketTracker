namespace TicketTracker.Application.MerchantAccounts.Commands;

public class ChangeAccountCommand : IRequest<CommandResult<MerchantAccount>>
{
    public Guid MerchantAccountId { get; set; }

    public Guid NewAccountId { get; set; }
}