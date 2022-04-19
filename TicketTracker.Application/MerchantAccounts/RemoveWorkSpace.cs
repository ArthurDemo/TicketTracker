using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Application.MerchantAccounts.Transformers;
using TicketTracker.Entity;
using TicketTracker.Entity.Exceptions;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts
{
    public class RemoveWorkSpace : IRequestHandler<RemoveWorkSpaceCommand, CommandResult>
    {
        private readonly IMerchantAccountRepository _merchantAccountRepository;

        public RemoveWorkSpace(IMerchantAccountRepository merchantAccountRepository)
        {
            _merchantAccountRepository = merchantAccountRepository;
        }

        public Task<CommandResult> Handle(RemoveWorkSpaceCommand command, CancellationToken cancellationToken)
        {
            var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

            if (merchantAccount == null) throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

            merchantAccount.RemoveWorkSpace(WorkSpaceTransform.ExtractWorkSpaceParameter(command));

            _merchantAccountRepository.Update(merchantAccount);

            return Task.FromResult(new CommandResult(true));
        }
    }
}