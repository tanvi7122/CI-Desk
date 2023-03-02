using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CIPlatform.Repository.Interface;
using CIPlatform.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace CIPLATFORM.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IConfiguration _config
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

        private string GenerateToken(User user)
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
      new Claim(ClaimTypes.Email,user.Email)
};
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // To Send Email
        public void SendEmail(string email, UserEmailOptions userEmailOptions)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_smtpConfigModel.SenderDisplayName, _smtpConfigModel.SenderAddress));
            message.To.Add(new MailboxAddress("User", email));

            message.Subject = userEmailOptions.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = userEmailOptions.Body;
            message.Body = bodyBuilder.ToMessageBody();
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_smtpConfigModel.host, smtpConfigModel.Port, smtpConfigModel.EnableSSL);
                smtp.Authenticate(_smtpConfigModel.UserName, _smtpConfigModel.Password);
                smtp.Send(message);
                smtp.Disconnect(true);
            }
        }

    }
}