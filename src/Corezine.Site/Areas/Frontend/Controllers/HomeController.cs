using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Corezine.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Corezine.Site.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IFeedbackService Feedback { get; }

        public HomeController(ILogger<HomeController> logger, IFeedbackService feedback)
        {
            _logger = logger;
            Feedback = feedback;
        }
        public IActionResult Index()
        {
            Feedback.Add(Services.Enumrations.FeedbackType.Error, "This is from another page");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}
