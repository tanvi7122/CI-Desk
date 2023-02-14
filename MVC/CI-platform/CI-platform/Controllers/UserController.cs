using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class UserController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
