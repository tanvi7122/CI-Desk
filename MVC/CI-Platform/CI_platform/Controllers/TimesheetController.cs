using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class TimesheetController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;
            private readonly ITimeSheetLandingRepository _TimeSheetLandingRepository;
          

            public TimesheetController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ITimeSheetLandingRepository TimeSheetLandingRepository)
            {
                _unitOfWork = unitOfWork;

                this._logger = logger;
            _TimeSheetLandingRepository = TimeSheetLandingRepository;

            }
            public IActionResult DataTable()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }


            TimeSheetVM GetTimeSheetPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
            return View(GetTimeSheetPageData);
           
        }
    }
}
