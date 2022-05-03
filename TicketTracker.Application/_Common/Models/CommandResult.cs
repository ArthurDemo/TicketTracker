namespace TicketTracker.Application._Common.Models;

public record CommandResult<T>(T? Entity, bool IsSuccess = true)
    where T : new()
{
    public CommandResult()
        : this(new T())
    {
    }

    public T? Entity { get; set; } = Entity;

    public bool IsSuccess { get; set; } = IsSuccess;
}