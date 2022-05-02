using System.Linq.Expressions;

using TicketTracker.Entity;
using TicketTracker.Entity.PrimitiveTypes;
using TicketTracker.Entity.Repositories;

namespace TicketTracker.Infrastructure.DataBase;

public class InMemoryRepository<T, TId>
: IRepository<T, TId>
    where TId : ObjectId<TId>
{
    private static readonly ThreadLocal<Dictionary<TId, T>> DataBase = new(() => new Dictionary<TId, T>());
    private readonly Dictionary<TId, T> _dataBase;

    public InMemoryRepository()
    {
        _dataBase = DataBase.Value!;
    }

    public IEnumerable<T>? Get(Expression<Func<T, bool>> filterSpec, int pageNo, int pageSize)
        => _dataBase.Values.Where(filterSpec.Compile()).Skip((pageNo - 1) * pageSize).Take(pageSize);

    public T? GetBy(Expression<Func<T, bool>> filterSpec)
        => _dataBase.Values.FirstOrDefault(filterSpec.Compile());

    public T? GetById(TId id)
        => _dataBase.TryGetValue(id, out var value) ? value : default;

    public void Add(T entity, TId? id = null)
        => _dataBase.TryAdd(id!, entity);

    public void Update(T entity, TId? id = null)
        => _dataBase[id!] = entity!;

    public void Delete(T entity)
        => _dataBase.Remove(_dataBase.First(o => o.Value!.Equals(entity)).Key);

    public void DeleteById(TId id)
        => _dataBase.Remove(id);
}

public class MerchantAccountRepository : InMemoryRepository<MerchantAccount, MerchantAccountId>, IMerchantAccountRepository

{ }

public class ProjectRepository : InMemoryRepository<Project, ProjectId>, IProjectRepository
{
}