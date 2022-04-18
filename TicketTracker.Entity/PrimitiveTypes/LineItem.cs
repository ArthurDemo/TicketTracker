namespace TicketTracker.Entity.PrimitiveTypes
{
    public abstract record LineItem(Guid Id, string Name)
    {
        public Guid Id { get; private set; } = Id;

        public string Name { get; private set; } = Name;
    }
}