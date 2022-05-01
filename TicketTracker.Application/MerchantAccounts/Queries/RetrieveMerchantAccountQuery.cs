using TicketTracker.Application.MerchantAccounts.DTOs;

namespace TicketTracker.Application.MerchantAccounts.Queries;

public record RetrieveMerchantAccountQuery : IRequest<MerchantAccountResult>
{
    public Guid MerchantAccountId { get; set; }
}