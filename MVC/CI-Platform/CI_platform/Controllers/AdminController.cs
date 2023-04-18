using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class AdminController : Controller
    {
      
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;
            private readonly IHomeLandingRepository _HomeLandingRepository;
            private readonly IMissionLandingRepository _MissionLandingRepository;
             private readonly IAdminRepository _AdminRepository;
            private readonly IConfiguration _config;

            public AdminController(ILogger<HomeController> logger, IConfiguration config, IUnitOfWork unitOfWork, IHomeLandingRepository HomeLandingRepository, IAdminRepository AdminRepository,IMissionLandingRepository MissionLandingRepository)
            {
                _unitOfWork = unitOfWork;

                this._logger = logger;
                _HomeLandingRepository = HomeLandingRepository;
                _config = config;
                _MissionLandingRepository = MissionLandingRepository;
            _AdminRepository = AdminRepository;

            }
     
        public IActionResult admin_user()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
          
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM UserData = _AdminRepository.GetUserData(sessionValue);
            return View(UserData);
        }
        public IActionResult admin_story()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM StoryData = _AdminRepository.GetStoryData();
            return View(StoryData);
            
        }
        public IActionResult admin_mission()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM missiondata = _AdminRepository.GetMissionData();
            return View(missiondata);

        }
        public IActionResult admin_cms()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM CmsData = _AdminRepository.GetCmsData();
            return View(CmsData);
        }
        public IActionResult admin_mission_application()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM MissionApplicationsData = _AdminRepository.GetMissionApplicationsData();
            return View(MissionApplicationsData);
        }
        public IActionResult DeleteUser(int UserId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var user_deleted = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == UserId);/*GetAll().Where(u => u.UserId == userId);*/
            user_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM UserData = _AdminRepository.GetUserData(sessionValue);
             return RedirectToAction("admin_user", "Admin") ;
        }
        public IActionResult DeleteCms(int Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var cms_deleted = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == Id);/*GetAll().Where(u => u.UserId == userId);*/
            cms_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM CmsData = _AdminRepository.GetCmsData();
            return RedirectToAction("admin_cms", "Admin");
        }
        public IActionResult DeleteStory(int Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var story_deleted = _unitOfWork.Story.GetFirstOrDefault(s=>s.StoryId== Id);/*GetAll().Where(u => u.UserId == userId);*/
            story_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM StoryData = _AdminRepository.GetStoryData();
            return RedirectToAction("admin_story", "Admin");
        }
    }
}
