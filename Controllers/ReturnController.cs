using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AmazonLoginMvc.Models;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AmazonLoginMvc.Controllers
{
    public class ReturnController : Controller
    {
        private readonly ILogger<ReturnController> _logger;
        public ReturnController(ILogger<ReturnController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var values = HttpUtility.ParseQueryString(Request.QueryString.Value);

            return Redirect(TempData["alexaRedirectUrl"]+ "?state=" + TempData["alexaState"] + "&code=" + values["code"]);
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
