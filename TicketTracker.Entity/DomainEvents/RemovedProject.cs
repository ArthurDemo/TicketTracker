using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.DomainEvents
{
    public record RemovedProject(Guid MerchantAccountId, string WorkSpaceName, ProjectId ProjectId)
        : DomainEvent(Guid.NewGuid(), DateTimer.Now, 1);
}