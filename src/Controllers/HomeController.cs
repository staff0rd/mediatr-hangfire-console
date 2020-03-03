using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mediatr_hangfire_console.Models;
using Hangfire;
using Hangfire.Server;
using Hangfire.Console;
using MediatR;
using mediatr_hangfire_console.Commands;

namespace mediatr_hangfire_console.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("queue-job")]
        public IActionResult QueueJob()
        {
            BackgroundJob.Enqueue(() => TheJob(null));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("queue-command")]
        public IActionResult QueueCommand() 
        {
            _mediator.Enqueue(new PerformContextCommand());
            return RedirectToAction("Index");
        }
        public void TheJob(PerformContext context)
        {
            context.WriteLine("A console message!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
