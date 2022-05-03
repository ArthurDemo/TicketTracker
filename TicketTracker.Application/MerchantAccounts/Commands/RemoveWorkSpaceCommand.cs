namespace TicketTracker.Application.MerchantAccounts.Commands;

public class RemoveWorkSpaceCommand : IRequest<CommandResult<MerchantAccount>>
{
    public Guid MerchantAccountId { get; init; }

    public string WorkSpaceName { get; init; } = null!;
}