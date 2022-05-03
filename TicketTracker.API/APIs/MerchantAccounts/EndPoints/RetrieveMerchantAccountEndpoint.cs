using MediatR;

using TicketTracker.Application.MerchantAccounts.DTOs;
using TicketTracker.Application.MerchantAccounts.Queries;

namespace TicketTracker.API.APIs.MerchantAccounts.EndPoints;

public class RetrieveMerchantAccountEndpoint : Endpoint<RetrieveMerchantAccountQuery, MerchantAccountResult>
{
    private readonly IMediator _mediator;

    public RetrieveMerchantAccountEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/merchantAccount/{MerchantAccountId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RetrieveMerchantAccountQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        await SendAsync(result!, cancellation: cancellationToken);
    }
}