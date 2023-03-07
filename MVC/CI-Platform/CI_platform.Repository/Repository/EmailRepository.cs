
using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public readonly CiPlatformContext _ciPlatformContext;
        public EmailRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }
    
        void IEmailRepository.EmailGeneration(ForgotPasswordValidation obj)
        {


            Random random = new Random();

            int capitalCharCode = random.Next(65, 91);
            char randomCapitalChar = (char)capitalCharCode;


            int randomint = random.Next();


            int SmallcharCode = random.Next(97, 123);
            char randomChar = (char)SmallcharCode;

            String token = "";
            token += randomCapitalChar.ToString();
            token += randomint.ToString();
            token += randomChar.ToString();


            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "localhost";
            uriBuilder.Port = 44351;
            uriBuilder.Path = "Home/Reset";
            uriBuilder.Query = "token=" + token;

            var PasswordResetLink = uriBuilder.ToString();

            var ResetPasswordInfo = new PasswordReset()
            {
                Email = obj.Email,
                Token = token
            };
            _ciPlatformContext.PasswordResets.Add(ResetPasswordInfo);
            _ciPlatformContext.SaveChanges();


            var fromEmail = new MailAddress("tanvizankat@gmail.com");
            var toEmail = new MailAddress(obj.Email);
            var fromEmailPassword = "xpquelppifdzvdpt";
            string subject = "Reset Password";
            string body = PasswordResetLink;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            MailMessage message = new MailMessage(fromEmail, toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}