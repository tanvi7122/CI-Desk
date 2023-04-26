using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CI_platform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IHomeLandingRepository _HomeLandingRepository;
        public readonly IAccountRepository _AccountRepo;

        public UserController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IAccountRepository AccountRepository, IHomeLandingRepository HomeLandingRepository)
        {
            _unitOfWork = unitOfWork;

            this._logger = logger;
            _HomeLandingRepository = HomeLandingRepository;

        }
        public bool IsLinkedInUrl(string url)
        {
            // Regular expression to match LinkedIn URLs
            string pattern = @"^https?://(www\.)?linkedin\.com/";

            // Use Regex.IsMatch to check if the URL matches the pattern
            return Regex.IsMatch(url, pattern);
        }

        public IActionResult GetCities(long country)
        {
            var cities = _unitOfWork.City.GetAll().Where(c => c.CountryId == country).ToList();
            return Json(cities);
        }

        [Authorize(Roles = "user")]
        public IActionResult Profile()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            HomeLandingPageVM UserProfilePageData = _HomeLandingRepository.GetUserProfileData(sessionValue);
            return View(UserProfilePageData);
            }
        [HttpPost]
        public IActionResult UserSkill(long UserId, long[] SkillIds)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
         
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }


            _unitOfWork.UserSkill.RemoveRange(UserId);
            foreach (var skillId in SkillIds)
            {
                var userSkill = new UserSkill
                {
                   UserId=UserId,
                    SkillId = skillId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
               
                _unitOfWork.UserSkill.Add(userSkill);
            }

            _unitOfWork.Save();

            return Ok();
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public IActionResult Profile(User userProfile, long UserId)
        {
            try
            {
                // Update the user profile in the database
                User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == UserId);
                if (user != null)
                {
                    bool isLinkedInUrl = IsLinkedInUrl(userProfile.LinkedInUrl);
                    if (isLinkedInUrl)
                    {
                        user.LinkedInUrl = userProfile.LinkedInUrl;
                    }
                    else
                    {
                        // URL is not a LinkedIn URL
                        return BadRequest("This is not a LinkedIn URL.");
                    }
                    user.WhyIVolunteer = userProfile.WhyIVolunteer;
                    user.Department = userProfile.Department;
                    user.ProfileText = userProfile.ProfileText;
            
                    user.Title = userProfile.Title;
                    user.CityId = userProfile.CityId;
                    user.CountryId = userProfile.CountryId;
                    user.FirstName = userProfile.FirstName;
                    user.LastName = userProfile.LastName;
                    user.EmployeeId = userProfile.EmployeeId;
                    

                    _unitOfWork.Save();
               
                }

                // Return a success response
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle the exception and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult UploadAvatar(IFormFile avatarFile, long UserId)
        {
            // Check if a file was uploaded
            var userId = Convert.ToInt64(UserId);
            if (avatarFile != null && avatarFile.Length > 0)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);

                // Get the path to the user's avatar folder
                var avatarFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatar");

                // If the user doesn't have an avatar folder, create one
                //if (!Directory.Exists(avatarFolder))
                //{
                //    Directory.CreateDirectory(avatarFolder);
                //}

                // Save the uploaded file to the user's avatar folder
                var filePath = Path.Combine(avatarFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatarFile.CopyTo(fileStream);
                }

                // Update the user's avatar path in the database
                var user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
                user.Avatar = "/avatar/" + fileName;
                _unitOfWork.Save();

                // Return the updated avatar path
               
              
                return Json(new { avatar = user.Avatar });

            }

            // Return an error message if no file was uploaded
            return Json(new { error = "No file was uploaded" });
        }
        public IActionResult ChangePassword(string currentPassword,string newPassword,String ConfirmPassword)
        
        {
            var userid = HttpContext.Session.GetString("UserId");
            long UserId = Convert.ToInt64(userid);
            var LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == UserId);
            bool isMatch = BCrypt.Net.BCrypt.Verify(currentPassword, LoggedUser.Password);
            if (isMatch)
                {
                Console.WriteLine("new password match");
                if (currentPassword != newPassword)
                {
                    if (newPassword == ConfirmPassword)
                    {
                        string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        newPassword = passwordHash;
                        _unitOfWork.User.UpdatePassword(LoggedUser, newPassword);
                        _unitOfWork.Save();
                    }
                    else
                    {
                        TempData["error"] = "Your newpassword and confirm password not match";
                        return RedirectToAction("profile");
                    }

                }
                else {
                    TempData["error"] = "you have entered Old password";
                    return RedirectToAction("profile");
                }
            
               
            }
            else
            {

                TempData["error"] = "You have Entered Wrong Old Password";
                return RedirectToAction("profile");
            }

            return RedirectToAction("profile");
        }

    }
}