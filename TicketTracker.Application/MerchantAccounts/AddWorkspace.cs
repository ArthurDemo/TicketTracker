using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Application.MerchantAccounts.Transformers;
using TicketTracker.Entity;
using TicketTracker.Entity.Exceptions;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Application.MerchantAccounts
{
    public class AddWorkSpace : IRequestHandler<AddWorkSpaceCommand, CommandResult>
    {
        private readonly IMerchantAccountRepository _merchantAccountRepository;

        public AddWorkSpace(IMerchantAccountRepository merchantAccountRepository)
        {
            _merchantAccountRepository = merchantAccountRepository;
        }

        public Task<CommandResult> Handle(AddWorkSpaceCommand command, CancellationToken cancellationToken)
        {
            var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

            if (merchantAccount == null) throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

            merchantAccount.AddWorkSpace(WorkSpaceTransform.ExtractWorkSpaceParameter(command));

            _merchantAccountRepository.Update(merchantAccount);

            return Task.FromResult(new CommandResult());
        }
    }
}