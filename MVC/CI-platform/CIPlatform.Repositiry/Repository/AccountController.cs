using CIPlatform.Models;
using CIPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUser(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
