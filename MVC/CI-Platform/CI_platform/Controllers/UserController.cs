using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCities(long country)
        {
            var cities = _unitOfWork.City.GetAll().Where(c => c.CountryId == country).ToList();
            return Json(cities);
        }
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

     

        [HttpPost]
        public IActionResult Profile(User userProfile, long UserId)
        {
            try
            {
            

                // Update the user profile in the database
                User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == UserId);
                if (user != null)
                {
                    user.WhyIVolunteer = userProfile.WhyIVolunteer;
                    user.Department = userProfile.Department;
                    user.ProfileText = userProfile.ProfileText;
                    user.LinkedInUrl = userProfile.LinkedInUrl;
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
            if (LoggedUser.Password == currentPassword)
                {
                Console.WriteLine("new password match");
                if (currentPassword != newPassword)
                {
                    if (newPassword == ConfirmPassword)
                    {
                        _unitOfWork.User.UpdatePassword(LoggedUser, newPassword);
                        _unitOfWork.Save();
                    }
                    else 
                    {
                        TempData["error"] = "Your newpassword and confirm password not match";
                        return RedirectToAction("profile");
                    }
         
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