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
            public IActionResult admin_mission()
        {
            return View();
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
            return View();
        }
        public IActionResult cms_page()
        {
            return View();
        }
    }
}
