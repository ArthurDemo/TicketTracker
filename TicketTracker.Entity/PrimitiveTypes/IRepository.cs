using System.Linq.Expressions;

namespace TicketTracker.Entity.PrimitiveTypes;

public interface IRepository<T, in TId>
{
    IEnumerable<T>? Get(Expression<Func<T, bool>> filterSpec, int pageNo, int pageSize);

    T? GetBy(Expression<Func<T, bool>> filterSpec);

    T? GetById(TId id);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    void DeleteById(TId id);
}