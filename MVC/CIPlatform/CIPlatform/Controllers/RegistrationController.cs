using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Repository.Interface;
using CIPlatform.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class RegistrationController : Controller
    {
        public readonly IAccountRepository _userobj;

        public RegistrationController(IAccountRepository userobj)
        {
            _userobj = userobj;
           
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {   
            var curser = _userobj.GetUserEmail(user.Email);
            Console.WriteLine(user.Email);
            if (curser != null && curser.Password == user.Password )
            {
                Console.WriteLine("Login successfull");
                TempData["success"] = "Login Successful";
                return RedirectToAction("RegistrationPage");
            }
            else
            {
                Console.WriteLine("Login Unsuccessfull");
                TempData["error"] = "login failed ";
                return View();
            }

        }


        public IActionResult Registrationpage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrationpage(User obj)
        {
            if (ModelState.IsValid)
            {
                _userobj.Add(obj);
                _userobj.save();
                TempData["success"] = "Register Successful";
                return RedirectToAction("Login");
            }
            TempData["error"] = "Registration failed";
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Forgot_pw()
        {
            return View();
        }

        //public IActionResult RegistrationPage()
        //{
        //    return View();
        //}
    }
}