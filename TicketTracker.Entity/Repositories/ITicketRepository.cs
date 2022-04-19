using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity.Repositories
{
    public interface ITicketRepository : IRepository<Ticket, TicketId>
    {
    }
}