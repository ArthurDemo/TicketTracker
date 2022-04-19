using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.DomainEvents
{
    public record AddedProject(Guid MerchantAccountId, string WorkSpaceName, ProjectId ProjectId)
        : DomainEvent(Guid.NewGuid(), DateTimer.Now, 1);
}