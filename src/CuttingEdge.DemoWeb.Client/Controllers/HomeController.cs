using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CuttingEdge.DemoWeb.Client.Controllers
{
    public class HomeController : Controller
    {
        private IConfigurationRoot _config;

        public HomeController(IConfigurationRoot config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Comment()
        {
            ViewData["Message"] = "Helow, React!";

            return View();
        }
    }
}
