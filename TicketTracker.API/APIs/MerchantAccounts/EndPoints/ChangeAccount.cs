using MediatR;

using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Entity;

namespace TicketTracker.API.APIs.MerchantAccounts.EndPoints;

public class ChangeAccount : Endpoint<ChangeAccountCommand, CommandResult<MerchantAccount>>
{
    private readonly IMediator _mediator;

    public ChangeAccount(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.PATCH);
        Routes("/merchantAccount/account");
    }

    public override async Task HandleAsync(ChangeAccountCommand cmd, CancellationToken cancellationToken)
    {
        await SendAsync(await _mediator.Send(cmd, cancellationToken), cancellation: cancellationToken);
    }
}