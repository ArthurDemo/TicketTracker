using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Entity;
using TicketTracker.Entity.Exceptions;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts
{
    public class ChangeAccount : IRequestHandler<ChangeAccountCommand, CommandResult>
    {
        private readonly IMerchantAccountRepository _merchantAccountRepository;

        public ChangeAccount(IMerchantAccountRepository merchantAccountRepository)
        {
            _merchantAccountRepository = merchantAccountRepository;
        }

        public Task<CommandResult> Handle(ChangeAccountCommand command, CancellationToken cancellationToken)
        {
            var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

            if (merchantAccount == null) throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

            merchantAccount.ChangeAccount(new AccountId(command.NewAccountId));
            _merchantAccountRepository.Update(merchantAccount);

            return Task.FromResult(new CommandResult());
        }
    }
}