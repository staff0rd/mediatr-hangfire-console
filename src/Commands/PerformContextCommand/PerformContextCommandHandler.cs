using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;

namespace mediatr_hangfire_console.Commands
{
    public class PerformContextCommandHandler : IRequestHandler<PerformContextCommand, Unit>
    {
        public PerformContextCommandHandler(ILogger<PerformContextCommandHandler> logger)
        {
            Console.WriteLine("Constructed");
        }
        public Task<Unit> Handle(PerformContextCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Unit());
        }
    }
}