using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.DTOs;

namespace TicketTracker.Application.MerchantAccounts.Queries;

public record FetchMerchantAccountQuery(int PageNo, int PageSize)
    : IRequest<PaginatedList<MerchantAccountResult>>;