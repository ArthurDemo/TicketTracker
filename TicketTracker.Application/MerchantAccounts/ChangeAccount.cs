using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.MerchantAccounts;

public class ChangeAccount : IRequestHandler<ChangeAccountCommand, CommandResult<MerchantAccount>>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public ChangeAccount(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task<CommandResult<MerchantAccount>> Handle(ChangeAccountCommand command, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

        MerchantAccountCouldNotFoundException.ThrowIfNull(merchantAccount);

        merchantAccount!.ChangeAccount(new AccountId(command.NewAccountId));
        _merchantAccountRepository.Update(merchantAccount);

        return Task.FromResult(new CommandResult<MerchantAccount>(merchantAccount!));
    }
}