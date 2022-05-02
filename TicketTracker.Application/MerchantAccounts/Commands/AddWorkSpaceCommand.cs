namespace TicketTracker.Application.MerchantAccounts.Commands;

public record AddWorkSpaceCommand : IRequest<CommandResult>
{
    public Guid MerchantAccountId { get; init; }

    public WorkSpaceParameter WorkSpace { get; init; } = null!;
}