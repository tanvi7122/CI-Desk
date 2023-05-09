using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using CI_Platform.Authentication;
using Data_Access.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace CI_platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IAccountRepository _AccountRepo;
        public readonly IEmailRepository _emailobj;
        public readonly CiPlatformContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandingPageRepository _LandingPageRepository;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ILandingPageRepository LandingPageRepository, IAccountRepository AccountRepository, IEmailRepository emailobj, CiPlatformContext context, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailobj = emailobj;
            _context = context;
            _AccountRepo = AccountRepository;
            _configuration = configuration;
            _LandingPageRepository = LandingPageRepository;
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
            LandingPageVM BannerPageData = _LandingPageRepository.GetBannerPageData();
            return View(BannerPageData);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Remove all keys and values from session
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the current user
            return RedirectToAction("Login", "Home"); // Redirect the user to the homepage
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(LandingPageVM profile)
        {
            //var Temp = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            var curser = _AccountRepo.GetUserEmail(profile.Email);

            bool isMatch = BCrypt.Net.BCrypt.Verify(profile.Password, curser.Password);
            if (curser != null && isMatch)
            {
                if (curser.Status == 1 && curser.DeletedAt == null)
                {
                    HttpContext.Session.SetString("UserEmail", profile.Email);
                    HttpContext.Session.SetString("UserId", curser.UserId.ToString());
                    var jwtSettings = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
                    var token = JwtTokenHelper.GenerateToken(jwtSettings, curser);
                    HttpContext.Session.SetString("Token", token);
                    if (curser.Role == "user")
                    {
                        return RedirectToAction("HomePage", "Mission");
                    }
                    else
                    {
                        return RedirectToAction("admin_user", "Admin");
                    }
                }
                else
                {
                    TempData["error"] = ToastrMessages.ErrorMessage;
                    return View("Login");
                }
            }
            else
            {

                TempData["error"] = ToastrMessages.InvalidPasswordMessage;
                return View("Login");
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

        public IActionResult Registration(LandingPageVM Profile, IFormCollection form)
        {
            var userphone = _context.Users.FirstOrDefault(t => t.PhoneNumber == Profile.PhoneNumber);

            {
                if (userphone == null)
                {

                    if (form["ConfirmPassword"] != Profile.Password && Profile.Password != null)
                    {
                        TempData["error"] = ToastrMessages.InvalidConfirmPasswordMessage;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Profile.Password))
                        {
                            bool isValidPassword = ValidatePassword(Profile.Password);
                            if (!isValidPassword)
                            {
                                // Password is not valid
                                ModelState.AddModelError("password", "Password must contain at least 8 characters, including at least one letter and one number.");
                            }
                            else
                            {
                                // Password is valid
                                string passwordHash = BCrypt.Net.BCrypt.HashPassword(Profile.Password);
                                Profile.Password = passwordHash;
                                string imagePath = Url.Content("~/images/user1.png");
                                Profile.Role = "user";
                                Profile.Avatar = imagePath;

                                User user = new User();
                                user.Password = passwordHash;
                                user.Role = "user";
                                user.Avatar = imagePath;
                                user.Email = Profile.Email;
                                user.FirstName = Profile.FirstName;
                                user.LastName = Profile.LastName;
                                _unitOfWork.User.Add(user);
                                _unitOfWork.Save();
                                TempData["error"] = ToastrMessages.SuccessMessage;
                                return RedirectToAction("Login");
                            }
                        }

                    }


                }
                else
                {
                    TempData["error"] = ToastrMessages.UserAlreadyExistsMessage;
                }
            }
            //else
            //{
            //    TempData["error"] = ToastrMessages.Required;
            //}


            return View();
        }

        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }


        //POST
        [HttpPost]
        public IActionResult Forgot(LandingPageVM obj)
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
                    TempData["error"] = ToastrMessages.InvalidPasswordMessage;
                    return View(obj);
                }
            }
            return RedirectToAction("Login", "Home");
        }





    }
}