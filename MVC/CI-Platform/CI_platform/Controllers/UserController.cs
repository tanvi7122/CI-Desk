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
        public UserController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IHomeLandingRepository HomeLandingRepository)
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
        public IActionResult UserProfile()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

            //HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sort, sessionValue,currentPage);
            HomeLandingPageVM UserProfilePageData = _HomeLandingRepository.GetUserProfileData(sessionValue);


            return View(UserProfilePageData);
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
                var avatarFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatar");

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

    }
}