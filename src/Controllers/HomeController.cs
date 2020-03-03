using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mediatr_hangfire_console.Models;
using Hangfire;
using Hangfire.Server;
using Hangfire.Console;
using MediatR;
using mediatr_hangfire_console.Commands;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Hangfire.Common;

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
            _mediator.Enqueue(new LoggingCommand { Message = "Log this to console plz!"});
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
