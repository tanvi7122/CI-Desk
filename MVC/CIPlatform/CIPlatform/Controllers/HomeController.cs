using CIPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIplatform.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       

        public IActionResult Forgot_pw()
        {
            return View();
        }

        //public IActionResult RegistrationPage()
        //{
        //    return View();
        //}
        public IActionResult reset()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new CIPlatform.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}