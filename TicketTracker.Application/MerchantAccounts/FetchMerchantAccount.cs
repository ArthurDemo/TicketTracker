using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.DTOs;
using TicketTracker.Application.MerchantAccounts.Queries;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts
{
    public class FetchMerchantAccount : IRequestHandler<FetchMerchantAccountQuery, PaginatedList<MerchantAccountResult>>
    {
        private readonly IMerchantAccountRepository _merchantAccountRepository;

        public FetchMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
        {
            _merchantAccountRepository = merchantAccountRepository;
        }

        public Task<PaginatedList<MerchantAccountResult>> Handle(FetchMerchantAccountQuery query, CancellationToken cancellationToken)
        {
            var merchantAccounts = _merchantAccountRepository
                .Get(o => true, query.PageNo, query.PageSize)!
                .Select(o => new MerchantAccountResult(o.Id, o.Account, o.WorkSpaces!))
                .ToList();

            return Task.FromResult(new PaginatedList<MerchantAccountResult>(
                merchantAccounts, merchantAccounts.Count, query.PageNo, query.PageSize));
        }
    }
}