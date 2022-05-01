using System.Runtime.CompilerServices;

namespace TicketTracker.Entity.PrimitiveTypes;

[Serializable]
public class ObjectNullException<T> : ArgumentException
    where T : ObjectNullException<T>, new()
{
    private static readonly ThreadLocal<Dictionary<string, Exception>> ExceptionCache = new(() => new Dictionary<string, Exception>());

    public ObjectNullException()
    {
    }

    public ObjectNullException(string entityName, string parameterName, Exception? innerException = null)
        : base(parameterName, $"找不到指定的{entityName}或不存在", innerException)
    {
    }

    public static void ThrowIfNull<TEntity>(TEntity? entity, [CallerArgumentExpression("entity")] string? parameterName = null)
    {
        if (entity is not null) return;

        var exceptionCache = ExceptionCache.Value;
        var exceptionName = typeof(T).Name;

        if (exceptionCache!.ContainsKey(exceptionName) == false)
            exceptionCache[exceptionName] = ((T)Activator.CreateInstance(typeof(T), parameterName)!);

        throw exceptionCache[exceptionName];
    }
}