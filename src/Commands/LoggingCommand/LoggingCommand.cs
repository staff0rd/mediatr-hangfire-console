using MediatR;

namespace mediatr_hangfire_console.Commands
{
    public class LoggingCommand : IRequest
    {
        public string Message { get; set; }
    }
}