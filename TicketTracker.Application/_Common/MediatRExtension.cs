namespace TicketTracker.Application._Common;

public static class MediatRExtension
{
    public static Task Publish<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
        where TDomainEvent : DomainEvent
    {
        return mediator.Publish(new DomainEventNotification<TDomainEvent>(domainEvent), cancellationToken);
    }
}