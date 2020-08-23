using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AmazonLoginMvc.Models;
using System.Web;

namespace AmazonLoginMvc.Controllers
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
            var values = HttpUtility.ParseQueryString(Request.QueryString.Value);
            TempData["alexaState"] = values["state"];
            TempData["alexaRedirectUrl"] = values["redirect_uri"];
            TempData["alexaScope"] = values["scope"];
            TempData["alexaClientId"] = values["client_id"];
            return View();
        }

        public IActionResult Privacy()
        {
            //test
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
