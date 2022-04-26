using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Entity;
using TicketTracker.Entity.Exceptions;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts;

public class RemoveMerchantAccount : IRequestHandler<RemoveMerchantAccountCommand, CommandResult>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public RemoveMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task<CommandResult> Handle(RemoveMerchantAccountCommand command, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));
        if (merchantAccount is null)
            throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

        _merchantAccountRepository.DeleteById(new MerchantAccountId(command.MerchantAccountId));

        return Task.FromResult(new CommandResult());
    }
}