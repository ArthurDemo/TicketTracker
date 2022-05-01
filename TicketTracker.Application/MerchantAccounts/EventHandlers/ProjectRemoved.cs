namespace TicketTracker.Application.MerchantAccounts.EventHandlers;

public class ProjectRemoved : INotificationHandler<DomainEventNotification<RemovedProject>>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;

    public ProjectRemoved(IMerchantAccountRepository merchantAccountRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
    }

    public Task Handle(DomainEventNotification<RemovedProject> notification, CancellationToken cancellationToken)
    {
        var merchantAccountId = new MerchantAccountId(notification.DomainEvent.MerchantAccountId);
        var merchantAccount = _merchantAccountRepository.GetById(merchantAccountId);
        if (merchantAccount is null) throw new MerchantAccountCouldNotFoundException(nameof(merchantAccount));

        var workSpace =
            merchantAccount.WorkSpaces!.FirstOrDefault(o => o.Name == notification.DomainEvent.WorkSpaceName);
        if (workSpace is null) throw new WorkSpaceCouldNotFoundException(nameof(workSpace));

        workSpace.RemoveProject(notification.DomainEvent.ProjectId);

        _merchantAccountRepository.Update(merchantAccount);

        return Task.CompletedTask;
    }
}