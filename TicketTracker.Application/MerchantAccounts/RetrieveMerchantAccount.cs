using MediatR;
using TicketTracker.Application.MerchantAccounts.DTOs;
using TicketTracker.Application.MerchantAccounts.Queries;
using TicketTracker.Entity;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts
{
    public class RetrieveMerchantAccount : IRequestHandler<RetrieveMerchantAccountQuery, MerchantAccountResult>
    {
        private readonly IMerchantAccountRepository _merchantAccountRepository;

        public RetrieveMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
        {
            _merchantAccountRepository = merchantAccountRepository;
        }

        public Task<MerchantAccountResult> Handle(RetrieveMerchantAccountQuery query, CancellationToken cancellationToken)
        {
            var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(query.AccountId));

            return Task.FromResult(new MerchantAccountResult(merchantAccount.Account, merchantAccount.WorkSpaces));
        }
    }
}