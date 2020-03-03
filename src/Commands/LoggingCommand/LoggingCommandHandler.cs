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

        public async Task<Unit> Handle(LoggingCommand request, CancellationToken cancellationToken)
        {
            var seconds = 10;
            do 
            {
                _logger.LogInformation(request.Message);
                await Task.Delay(2000);
                seconds--;
            } while (seconds > 0);
            _logger.LogError("Finished!");
            return Unit.Value;
        }
    }
}