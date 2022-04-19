using MediatR;
using TicketTracker.Application._Common.Models;
using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Application._Common
{
    public static class MediatRExtension
    {
        public static Task Publish<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent,
            CancellationToken cancellationToken = default)
            where TDomainEvent : DomainEvent
            => mediator.Publish(new DomainEventNotification<TDomainEvent>(domainEvent), cancellationToken);
    }
}