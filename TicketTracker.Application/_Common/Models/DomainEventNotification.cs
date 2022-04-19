﻿using MediatR;
using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Application._Common.Models
{
    public record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent)
        : INotification where TDomainEvent : DomainEvent
    {
    }
}