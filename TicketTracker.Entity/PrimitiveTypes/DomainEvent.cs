namespace TicketTracker.Entity.PrimitiveTypes;

public record DomainEvent(Guid EventId, DateTime CreatedDate, int Version)
{
}