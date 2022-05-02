using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Application.MerchantAccounts.Transformers;

namespace TicketTracker.Application.MerchantAccounts;

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

        MerchantAccountCouldNotFoundException.ThrowIfNull(merchantAccount);

        merchantAccount!.AddWorkSpace(WorkSpaceTransform.ExtractWorkSpaceParameter(command.WorkSpace));

        _merchantAccountRepository.Update(merchantAccount);

        return Task.FromResult(new CommandResult());
    }
}