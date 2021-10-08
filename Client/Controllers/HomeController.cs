using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
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
            var userIdentity = User.Identity.IsAuthenticated;
            if (userIdentity)
            {
                var payload = HttpContext.Session.GetString("Payload");

                var jsonPayload = JsonConvert.DeserializeObject(payload);

                ViewBag.Payload = jsonPayload;
                
                return View();
            }

            return RedirectToAction("Index", "Accounts");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Sample()
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
