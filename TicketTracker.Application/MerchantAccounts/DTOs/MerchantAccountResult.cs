namespace TicketTracker.Application.MerchantAccounts.DTOs;

public record MerchantAccountResult(MerchantAccountId Id, AccountId AccountId, List<WorkSpace> WorkSpaces)
{
    public MerchantAccountResult()
        : this(MerchantAccountId.Default, AccountId.Default, new List<WorkSpace>())
    {
    }

    public MerchantAccountId Id { get; set; } = Id;
    public AccountId AccountId { get; set; } = AccountId;
    public List<WorkSpace> WorkSpaces { get; set; } = WorkSpaces;
}