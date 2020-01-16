using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using miniprofiler.Models;
using StackExchange.Profiling;

namespace miniprofiler.Controllers
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
            using (MiniProfiler.Current.Step("Example Step"))
            {
                using (MiniProfiler.Current.Step("Sub timing"))
                {
                    // Not trying to delay the page load here, only serve as an example
                    Thread.Sleep(100);
                    _logger.LogDebug("in the profiler");
                }
                using (MiniProfiler.Current.Step("Sub timing 2"))
                {
                    // Not trying to delay the page load here, only serve as an example
                    Thread.Sleep(200);
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
