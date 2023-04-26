using CI_platfom.Entity.Models;

using Microsoft.AspNetCore.Identity;
using CI_platform.Repository.Interface;
using CI_platform.Repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CI_platfom.Entity.ViewModel;
using CI_platfom.Entity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CI_Platform.Authentication;
using Data_Access.Auth;

namespace CI_platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IAccountRepository _AccountRepo;
        public readonly IEmailRepository _emailobj;
        public readonly CiPlatformContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IAccountRepository AccountRepository, IEmailRepository emailobj, CiPlatformContext context, IConfiguration configuration)
        {

            _logger = logger;
            _emailobj = emailobj;
            _context = context;
            _AccountRepo = AccountRepository;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (!String.IsNullOrEmpty(sessionValue))
            {
                Console.WriteLine(sessionValue);
                return RedirectToAction("HomePage", "Mission");
            }
            return View("Login");
        }
 
        public IActionResult Login()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (!String.IsNullOrEmpty(sessionValue))
            {
                Console.WriteLine(sessionValue);
                return RedirectToAction("HomePage", "Mission");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Remove all keys and values from session
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the current user
            return RedirectToAction("Login", "Home"); // Redirect the user to the homepage
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(User user)
        {
            //var Temp = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            var curser = _AccountRepo.GetUserEmail(user.Email);
          
            bool isMatch = BCrypt.Net.BCrypt.Verify(user.Password,curser.Password);
            if (curser != null && isMatch)
            {
             
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserId", curser.UserId.ToString());
                var jwtSettings = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
                var token = JwtTokenHelper.GenerateToken(jwtSettings, curser);
                HttpContext.Session.SetString("Token", token);
                if (curser.Role == "user")
                {
                    return RedirectToAction("HomePage", "Mission");
                }
                else {
                    return RedirectToAction("admin_user", "Admin");
                }
            }
            else
            {
               
                ViewData["ErrorMessage"] = "Login Failed";
                return View(user);
            }
        }
     private bool ValidatePassword(string password)
{
    // Password must contain at least 8 characters, including at least one letter and one number
    return password.Length >= 8 && password.Any(char.IsLetter) && password.Any(char.IsDigit);
}

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Registration(User user, IFormCollection form)
        {
            var userphone = _context.Users.FirstOrDefault(t => t.PhoneNumber == user.PhoneNumber);
            if (ModelState.IsValid && userphone == null)
            {

                if (form["ConfirmPassword"] != user.Password)
                {
                    ViewData["warning"] = "Passwords do not match";

                }
                else
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        bool isValidPassword = ValidatePassword(user.Password);
                        if (!isValidPassword)
                        {
                            // Password is not valid
                            ModelState.AddModelError("password", "Password must contain at least 8 characters, including at least one letter and one number.");
                        }
                        else
                        {
                            // Password is valid
                            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                            user.Password = passwordHash;
                            string imagePath = Url.Content("~/images/user1.png");
                            user.Role = "user";
                            user.Avatar = imagePath;
                            _AccountRepo.Add(user);
                            _AccountRepo.save();
                            ViewData["success"] = "Registration Successful";
                            return RedirectToAction("Login");
                        }
                    }
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "You already have an account. Please log in instead OR Fill all required details ";
                return View(user);
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }


        //POST
        [HttpPost]
        public IActionResult Forgot(ForgotPasswordValidation obj)
        {
            if (ModelState.IsValid)
            {
                User user = _AccountRepo.GetUserEmail(obj.Email);
                if (user == null)
                {
                    TempData["error"] = "User does not exist!!!";
                    return View(obj);
                }
                else
                {
                    _emailobj.EmailGeneration(obj);
                    TempData["success"] = "Link sent!!!";
                }
            }
            return View(obj);
        }
        public IActionResult Reset()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Reset(NewPasswordValidation obj, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (form["conformpassword"] == obj.Password)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                    obj.Password = passwordHash;

                    _AccountRepo.UpdateUser(obj);
                    TempData["success"] = "Password updated!!!";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    TempData["error"] = "password dosen't match";
                    return View(obj);
                }
            }
            return RedirectToAction("Login", "Home");
        }

       



    }
}