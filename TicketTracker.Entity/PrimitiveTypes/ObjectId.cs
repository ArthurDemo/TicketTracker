namespace TicketTracker.Entity.PrimitiveTypes
{
    public abstract record ObjectId<T>(Guid Id)
        where T : ObjectId<T>
    {
        public override string ToString()
         => Id.ToString();
    }
}