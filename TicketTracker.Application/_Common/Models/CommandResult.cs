namespace TicketTracker.Application._Common.Models;

public record CommandResult(bool IsSuccess = true)
{
    public CommandResult()
        : this(true)
    {
    }

    public bool IsSuccess { get; set; } = IsSuccess;
}