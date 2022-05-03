using MediatR;

using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Entity;

namespace TicketTracker.API.APIs.MerchantAccounts.EndPoints;

public class RegisterMerchantAccountEndPoint : Endpoint<RegisterMerchantAccountCmd, CommandResult<MerchantAccount>>
{
    private readonly IMediator _mediator;

    public RegisterMerchantAccountEndPoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/merchantAccount/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterMerchantAccountCmd cmd, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(cmd, cancellationToken);
        await SendCreatedAtAsync<RetrieveMerchantAccountEndpoint>(null, result, cancellation: cancellationToken);
    }
}