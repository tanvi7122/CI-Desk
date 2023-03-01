using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class userlogin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
public class userlogin : Controller
{
    private readonly UserService _userService;

    public userlogin(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };
            _userService.RegisterUser(user);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
}

