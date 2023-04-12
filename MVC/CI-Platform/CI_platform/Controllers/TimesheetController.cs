using CI_platfom.Entity.Models;
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
        [HttpPost]
        public IActionResult AddTimeSheet(addTimeSheet formData)
        {
            var userid = HttpContext.Session.GetString("UserId");
            long userId = Convert.ToInt64(userid);
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

            var timeSpan = TimeOnly.Parse(formData.Hour + ":" + formData.minutes);
            var timeSheet = new Timesheet
            {   UserId = userId,
                MissionId = formData.MissionId,
                DateVolunteered = formData.DateVolunteered,
                Time= timeSpan,
                Notes = formData.Notes
            };

            _unitOfWork.Timesheet.Add(timeSheet);
            _unitOfWork.Save();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AddGoalSheet(addTimeSheet formdata)
        {
            var userid = HttpContext.Session.GetString("UserId");
            long userId = Convert.ToInt64(userid);
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

   
            var GoalSheet = new Timesheet
            {
                UserId = userId,
                MissionId = formdata.MissionId,
                Action=formdata.Action,
                DateVolunteered = formdata.DateVolunteered,
                Notes = formdata.Notes
            };

            _unitOfWork.Timesheet.Add(GoalSheet);
            _unitOfWork.Save();

            return Json(new { success = true });
        }
        public ActionResult Gettimesheet()
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
