using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.MerchantAccounts;

public class RemoveMerchantAccount : IRequestHandler<RemoveMerchantAccountCommand, CommandResult>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public RemoveMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository=merchantAccountRepository;
    }

    public Task<CommandResult> Handle(RemoveMerchantAccountCommand command, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

        MerchantAccountCouldNotFoundException.ThrowIfNull(merchantAccount);

        _merchantAccountRepository.DeleteById(new MerchantAccountId(command.MerchantAccountId));

        return Task.FromResult(new CommandResult());
    }
}