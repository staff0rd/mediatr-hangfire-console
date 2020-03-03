using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;

namespace mediatr_hangfire_console.Commands
{
    public class LoggingCommandHandler : IRequestHandler<LoggingCommand, Unit>
    {
        private ILogger<LoggingCommandHandler> _logger;

        public LoggingCommandHandler(ILogger<LoggingCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(LoggingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.Message);
            return Task.FromResult(new Unit());
        }
    }
}