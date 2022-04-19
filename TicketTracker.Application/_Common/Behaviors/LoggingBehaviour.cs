using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace TicketTracker.Application._Common.Behaviors
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userName = string.Empty;

            _logger.LogInformation("TicketTracker Request: {Name} {@Request}",
                requestName, request);
        }
    }
}