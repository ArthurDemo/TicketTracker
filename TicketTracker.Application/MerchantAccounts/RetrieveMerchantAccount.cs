using TicketTracker.Application.MerchantAccounts.DTOs;
using TicketTracker.Application.MerchantAccounts.Queries;

namespace TicketTracker.Application.MerchantAccounts;

public class RetrieveMerchantAccount : IRequestHandler<RetrieveMerchantAccountQuery, MerchantAccountResult>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public RetrieveMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task<MerchantAccountResult> Handle(RetrieveMerchantAccountQuery query, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(query.MerchantAccountId));

        return Task.FromResult(new MerchantAccountResult(merchantAccount!.Id, merchantAccount!.Account,
            merchantAccount.WorkSpaces!));
    }
}