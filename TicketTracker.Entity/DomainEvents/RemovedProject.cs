using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.DomainEvents
{
    public record RemovedProject(Guid MerchantAccountId, string WorkSpaceName, ProjectId ProjectId, Guid? EventId = null)
        : DomainEvent(EventId ?? GuidMaker.NewGuid(), DateTimer.Now, 1);
}