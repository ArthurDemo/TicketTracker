using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.MerchantAccounts;

public class RemoveWorkSpace : IRequestHandler<RemoveWorkSpaceCommand, CommandResult>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public RemoveWorkSpace(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository=merchantAccountRepository;
    }

    public Task<CommandResult> Handle(RemoveWorkSpaceCommand command, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(command.MerchantAccountId));

        MerchantAccountCouldNotFoundException.ThrowIfNull(merchantAccount);
        WorkSpaceCouldNotFoundException.ThrowIfNull(merchantAccount!.WorkSpaces);

        if (merchantAccount.WorkSpaces!.FirstOrDefault(o => o.Name==command.WorkSpaceName) is var workSpace&&
            workSpace is null)
            throw new WorkSpaceCouldNotFoundException(nameof(workSpace));

        merchantAccount.RemoveWorkSpace(workSpace!);

        _merchantAccountRepository.Update(merchantAccount);

        return Task.FromResult(new CommandResult());
    }
}