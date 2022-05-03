using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Application.MerchantAccounts.Transformers;

namespace TicketTracker.Application.MerchantAccounts;

public class RegisterMerchantAccount : IRequestHandler<RegisterMerchantAccountCmd, CommandResult<MerchantAccount>>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public RegisterMerchantAccount(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task<CommandResult<MerchantAccount>> Handle(RegisterMerchantAccountCmd command, CancellationToken cancellationToken)
    {
        var (accountId, workSpaces) = ExtractMerchantAccountParameter(command);

        var merchantAccount = MerchantAccount.Create(accountId, workSpaces);
        _merchantAccountRepository.Add(merchantAccount!, merchantAccount!.Id);

        return Task.FromResult(new CommandResult<MerchantAccount>(merchantAccount));
    }

    private static (AccountId accountId, List<WorkSpace>? workSpaces) ExtractMerchantAccountParameter(
        RegisterMerchantAccountCmd command)
    {
        var accountId = new AccountId(command.AccountId);
        var workSpaces = command.WorkSpaces?
            .Select(WorkSpaceTransform.ExtractWorkSpaceParameter)
            .ToList();

        return (accountId, workSpaces);
    }
}