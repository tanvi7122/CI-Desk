using CI_platfom.Entity.Models;
using CI_platform.Models;
using Microsoft.AspNetCore.Identity;
using CI_platform.Repository.Interface;
using CI_platform.Repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CI_platfom.Entity.ViewModel;
using CI_platfom.Entity.Data;

namespace CI_platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IAccountRepository _AccountRepo;
        public readonly IEmailRepository _emailobj;
        public readonly CiPlatformContext _context;



        public HomeController(ILogger<HomeController> logger, IAccountRepository AccountRepository, IEmailRepository emailobj, CiPlatformContext context)
        {
            
            _logger = logger;
            _emailobj = emailobj;
            _context = context;
            _AccountRepo = AccountRepository;


        }

        public IActionResult Login()
        {
            return View();
        }

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {

            var curser = _AccountRepo.GetUserEmail(user.Email);
            Console.WriteLine(user.Email);
            if (curser != null && curser.Password == user.Password)
            {
                Console.WriteLine("Login successfull");
                TempData["success"] = "Login Successful";
                return RedirectToAction("Landing_page");
            }
            else
            {
                Console.WriteLine("Login Unsuccessfull");
                TempData["error"] = "login failed ";
                return View();
            }


        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Registration(User user, IFormCollection form)
        {
            if (ModelState.IsValid)
            {

                if (form["conformpassword"] == user.Password)
                {
                    _AccountRepo.Add(user);
                    _AccountRepo.save();
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = "password dosen't match";
                    return View(user);
                }
            }
            TempData["error"] = "Registration failed";
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