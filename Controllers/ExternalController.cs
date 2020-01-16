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
    public class ExternalController : Controller
    {
        private readonly ILogger<ExternalController> logger;
        Random random;  
        public ExternalController(ILogger<ExternalController> logger)
        {
            this.logger = logger;
            random = new Random();
        }

        [HttpGet]
        public IActionResult Ping()
        {
            var t2s = random.Next(1, 5);
            using (MiniProfiler.Current.Step("sleepy"))
                Thread.Sleep(t2s * 100);
                
            return new OkObjectResult(new { TimeSlept = t2s * 100/ 1000M, TodayIs = DateTime.Now });
        }
    }
}