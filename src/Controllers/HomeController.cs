using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mediatr_hangfire_console.Models;
using Hangfire;
using Hangfire.Server;
using Hangfire.Console;

namespace mediatr_hangfire_console.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
