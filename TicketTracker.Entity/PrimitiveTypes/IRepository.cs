using System.Linq.Expressions;

namespace TicketTracker.Entity.PrimitiveTypes;

public interface IRepository<T, in TId>
    where TId : ObjectId<TId>
{
    IEnumerable<T>? Get(Expression<Func<T, bool>> filterSpec, int pageNo, int pageSize);

    T? GetBy(Expression<Func<T, bool>> filterSpec);

    T? GetById(TId id);

    void Add(T entity, TId? id = null);

    void Update(T entity, TId? id = null);

    void Delete(T entity);

    void DeleteById(TId id);
}