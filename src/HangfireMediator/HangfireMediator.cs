using System.Threading.Tasks;
using Hangfire;
using Hangfire.Server;
using MediatR;
using mediatr_hangfire_console.Controllers;

// https://codeopinion.com/background-commands-mediatr-hangfire/
namespace mediatr_hangfire_console
{
    public static class MediatRExtension
    {
        public static void Enqueue(this IMediator mediator, IRequest request)
        {
            BackgroundJob.Enqueue<HangfireMediator>(m => m.SendCommand(request));
        }
    }

    [LogToHangfireConsole]
    public class HangfireMediator
    {
        private readonly IMediator _mediator;

        public HangfireMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand(IRequest request)
        {
            await _mediator.Send(request);
        }
    }
}