using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace TicketTracker.Entity.PrimitiveTypes;

[Serializable]
public class SpecException<T> : ArgumentException
    where T : SpecException<T>, new()
{
    private static readonly ThreadLocal<Dictionary<string, Exception>> ExceptionCache = new(() => new Dictionary<string, Exception>());

    public SpecException()
    {
    }

    public SpecException(string itemName, string failReason, string parameterName, Exception? innerException = null)
        : base(parameterName, $"規格項目:{itemName},檢查不符合, 錯誤可能原因為: {failReason}", innerException)
    {
    }

    public static void ThrowIfNotMatch<TEntity>(TEntity? entity,
        Expression<Predicate<TEntity>> spec,
        [CallerArgumentExpression("entity")] string? parameterName = null)
    {
        if (entity is not null && spec.Compile()(entity)) return;

        var exceptionCache = ExceptionCache.Value;
        var exceptionName = typeof(T).Name;

        if (exceptionCache!.ContainsKey(exceptionName) == false)
        {
            exceptionCache[exceptionName] = ((T)Activator.CreateInstance(typeof(T), parameterName)!);
        }

        throw exceptionCache[exceptionName];
    }
}