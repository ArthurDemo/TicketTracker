using System.Linq.Expressions;

using TicketTracker.Entity;
using TicketTracker.Entity.PrimitiveTypes;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Infrastructure.DataBase;

public class InMemoryRepository<T, TId>
: IRepository<T, TId>
    where TId : ObjectId<TId>
{
    private static readonly Dictionary<TId, T> DataBase = new();

    public IEnumerable<T>? Get(Expression<Func<T, bool>> filterSpec, int pageNo, int pageSize)
        => DataBase.Values.Where(filterSpec.Compile()).Skip((pageNo - 1) * pageSize).Take(pageSize);

    public T? GetBy(Expression<Func<T, bool>> filterSpec)
        => DataBase.Values.FirstOrDefault(filterSpec.Compile());

    public T? GetById(TId id)
        => DataBase.TryGetValue(id, out var value) ? value : default;

    public void Add(T entity, TId? id = null)
        => DataBase.TryAdd(id!, entity);

    public void Update(T entity, TId? id = null)
        => DataBase[id!] = entity!;

    public void Delete(T entity)
        => DataBase.Remove(DataBase.First(o => o.Value!.Equals(entity)).Key);

    public void DeleteById(TId id)
        => DataBase.Remove(id);
}

public class MerchantAccountRepository : InMemoryRepository<MerchantAccount, MerchantAccountId>, IMerchantAccountRepository

{ }

public class ProjectRepository : InMemoryRepository<Project, ProjectId>, IProjectRepository
{
}