﻿using MediatR;
using TicketTracker.Application.MerchantAccounts.DTOs;

namespace TicketTracker.Application.MerchantAccounts.Queries;

public record RetrieveMerchantAccountQuery : IRequest<MerchantAccountResult>
{
    public Guid AccountId { get; set; }
}