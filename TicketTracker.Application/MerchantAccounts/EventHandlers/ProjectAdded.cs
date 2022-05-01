namespace TicketTracker.Application.MerchantAccounts.EventHandlers;

public class ProjectAdded : INotificationHandler<DomainEventNotification<AddedProject>>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public ProjectAdded(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task Handle(DomainEventNotification<AddedProject> notification, CancellationToken cancellationToken)
    {
        var merchantAccountId = new MerchantAccountId(notification.DomainEvent.MerchantAccountId);
        var merchantAccount = _merchantAccountRepository.GetById(merchantAccountId);
        if (merchantAccount is null) throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

        var workSpace =
            merchantAccount.WorkSpaces!.FirstOrDefault(o => o.Name == notification.DomainEvent.WorkSpaceName);
        if (workSpace is null) throw new WorkSpaceCouldNotFoundException(nameof(workSpace));

        workSpace.AddProject(notification.DomainEvent.ProjectId);

        _merchantAccountRepository.Update(merchantAccount);

        return Task.CompletedTask;
    }
}