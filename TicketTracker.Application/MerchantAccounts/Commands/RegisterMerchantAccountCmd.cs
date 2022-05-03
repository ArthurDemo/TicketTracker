namespace TicketTracker.Application.MerchantAccounts.Commands;

public record RegisterMerchantAccountCmd : IRequest<CommandResult<MerchantAccount>>
{
    public Guid AccountId { get; set; }

    public IEnumerable<WorkSpaceParameter>? WorkSpaces { get; set; }
}

public record WorkSpaceParameter(string Name, IEnumerable<Guid> ProjectIds, uint UpperLimit);