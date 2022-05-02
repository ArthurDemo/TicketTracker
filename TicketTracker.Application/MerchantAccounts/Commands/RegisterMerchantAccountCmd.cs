namespace TicketTracker.Application.MerchantAccounts.Commands;

public record RegisterMerchantAccountCmd : IRequest<CommandResult>
{
    public Guid AccountId { get; set; }

    public IEnumerable<WorkSpaceParameter>? WorkSpaces { get; set; }
}

public record WorkSpaceParameter(string Name, IEnumerable<Guid> ProjectIds, uint UpperLimit);