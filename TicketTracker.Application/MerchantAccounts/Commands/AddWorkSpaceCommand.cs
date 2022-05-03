namespace TicketTracker.Application.MerchantAccounts.Commands;

public record AddWorkSpaceCommand : IRequest<CommandResult<MerchantAccount>>
{
    public Guid MerchantAccountId { get; init; }

    public WorkSpaceParameter WorkSpace { get; init; } = null!;
}