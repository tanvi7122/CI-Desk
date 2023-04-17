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

        public IActionResult Index()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (!String.IsNullOrEmpty(sessionValue))
            {
                Console.WriteLine(sessionValue);
                return RedirectToAction("HomePage", "Mission");
            }
            return View();
        }
        public IActionResult EndSession()
        {
            HttpContext.Session.Clear(); // Remove all keys and values from session
            return Ok();
        }

        public IActionResult Login()
        {
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserId");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Remove all keys and values from session
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the current user
            return RedirectToAction("login", "Home"); // Redirect the user to the homepage
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            var Temp = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            var curser = _AccountRepo.GetUserEmail(user.Email);
            Console.WriteLine(user.Email);
            if (curser != null && curser.Password == user.Password)
            {
                Console.WriteLine("Login successfull");
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserId", Temp.UserId.ToString());
                return RedirectToAction("HomePage", "Mission");
            }
            else
            {
                Console.WriteLine("Login Unsuccessfull");
                ViewData["ErrorMessage"] = "Login Failed";
                return View(user);
            }


        }

        private static byte[] GenerateKey(int keySize)
        {
            var rng = new RNGCryptoServiceProvider();
            var key = new byte[keySize / 8];
            rng.GetBytes(key);
            return key;
        }

        public static class EncryptionHelper
        {
            private const int SaltSize = 16;

            public static string Encrypt(string plainText, byte[] encryptionKey)
            {
                byte[] salt = new byte[SaltSize];
                byte[] iv = new byte[16];
                byte[] array;

                // Generate a random salt
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }

                using (Aes aes = Aes.Create())
                {
                    aes.Key = encryptionKey;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            // Write the salt to the output stream
                            cryptoStream.Write(salt, 0, salt.Length);

                            // Encrypt the plain text
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                // Combine the salt and encrypted data into a single base64 string
                byte[] combinedArray = new byte[SaltSize + array.Length];
                Buffer.BlockCopy(salt, 0, combinedArray, 0, SaltSize);
                Buffer.BlockCopy(array, 0, combinedArray, SaltSize, array.Length);

                return Convert.ToBase64String(combinedArray);
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
            var userphone = _context.Users.FirstOrDefault(t => t.PhoneNumber == user.PhoneNumber);
            if (ModelState.IsValid && userphone == null)
            {

                if (form["ConfirmPassword"] != user.Password)
                {
                    ViewData["warning"] = "Passwords do not match";

                }
                else
                {
                    // Generate the encryption key
                    byte[] encryptionKey = GenerateKey(256);

                    // Encrypt the password using the generated key
                    string encryptedPassword = EncryptionHelper.Encrypt(user.Password, encryptionKey);

                    // Set the encrypted password and key on the user object
                    user.Password = encryptedPassword;
                    //user.EncryptionKey = Convert.ToBase64String(encryptionKey);

                    string imagePath = Url.Content("~/images/user1.png");
                    user.Avatar = imagePath;
                    _AccountRepo.Add(user);
                    _AccountRepo.save();
                    ViewData["success"] = "Registration Successful";
                    return RedirectToAction("Login");

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