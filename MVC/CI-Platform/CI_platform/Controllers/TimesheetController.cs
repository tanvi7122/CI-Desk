//using CI_platfom.Entity.Models;
//using CI_platfom.Entity.ViewModel;
//using CI_platform.Repository.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace CI_platform.Controllers
//{
//    public class TimesheetController : Controller
//        {
//            private readonly IUnitOfWork _unitOfWork;
//            private readonly ILogger _logger;
//            private readonly ITimeSheetLandingRepository _TimeSheetLandingRepository;


//            public TimesheetController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ITimeSheetLandingRepository TimeSheetLandingRepository)
//            {
//                _unitOfWork = unitOfWork;

//                this._logger = logger;
//            _TimeSheetLandingRepository = TimeSheetLandingRepository;

//            }
//            public IActionResult DataTable()
//        {
//            var sessionValue = HttpContext.Session.GetString("UserEmail");
//            if (String.IsNullOrEmpty(sessionValue))
//            {
//                TempData["error"] = "Session Expired!\nPlease Login Again!";
//                return RedirectToAction("Index");
//            }


//            TimeSheetVM GetTimeSheetPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
//            return View(GetTimeSheetPageData);

//        }
//        [HttpPost]
//        public IActionResult AddTimeSheet(addTimeSheet formData)
//        {
//            var userid = HttpContext.Session.GetString("UserId");
//            long userId = Convert.ToInt64(userid);
//            var sessionValue = HttpContext.Session.GetString("UserEmail");
//            if (String.IsNullOrEmpty(sessionValue))
//            {
//                TempData["error"] = "Session Expired!\nPlease Login Again!";
//                return RedirectToAction("Index");
//            }

//            var timeSpan = TimeOnly.Parse(formData.Hour + ":" + formData.minutes);
//            var timeSheet = new Timesheet
//            {   UserId = userId,
//                MissionId = formData.MissionId,
//                DateVolunteered = formData.DateVolunteered,
//                Time= timeSpan,
//                Notes = formData.Notes
//            };

//            _unitOfWork.Timesheet.Add(timeSheet);
//            _unitOfWork.Save();

//            return Json(new { success = true });
//        }

//        [HttpPost]
//        public IActionResult AddGoalSheet(addTimeSheet formdata)
//        {
//            var userid = HttpContext.Session.GetString("UserId");
//            long userId = Convert.ToInt64(userid);
//            var sessionValue = HttpContext.Session.GetString("UserEmail");
//            if (String.IsNullOrEmpty(sessionValue))
//            {
//                TempData["error"] = "Session Expired!\nPlease Login Again!";
//                return RedirectToAction("Index");
//            }


//            var GoalSheet = new Timesheet
//            {
//                UserId = userId,
//                MissionId = formdata.MissionId,
//                Action=formdata.Action,
//                DateVolunteered = formdata.DateVolunteered,
//                Notes = formdata.Notes
//            };

//            _unitOfWork.Timesheet.Add(GoalSheet);
//            _unitOfWork.Save();

//            return Json(new { success = true });
//        }
//        public ActionResult Gettimesheet()
//        {
//            var sessionValue = HttpContext.Session.GetString("UserEmail");
//            if (String.IsNullOrEmpty(sessionValue))
//            {
//                TempData["error"] = "Session Expired!\nPlease Login Again!";
//                return RedirectToAction("Index");
//            }

//            TimeSheetVM GetTimeSheetPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
//            return View(GetTimeSheetPageData);
//        }

//    }
//}
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class TimeSheetController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeSheetLandingRepository _TimeSheetLandingRepository;
        private readonly IConfiguration _config;

        public TimeSheetController(IConfiguration config, IUnitOfWork unitOfWork, ITimeSheetLandingRepository TimeSheetLandingRepository)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _TimeSheetLandingRepository = TimeSheetLandingRepository;


        }
        public IActionResult TimeSheet()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
            ViewBag.userProfile = obj.Avatar;
            ViewBag.firstName = obj.FirstName;
            ViewBag.lastName = obj.LastName;
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            TimeSheetVM TimeSheetlandingPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
            return View("TimeSheet", TimeSheetlandingPageData);
        }

        [HttpPost]
        public IActionResult AddTimeTs(Timesheet Timesheet)

        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
          
            var missionobj = _unitOfWork.Mission.GetFirstOrDefault(m => m.MissionId == Timesheet.MissionId);
            var timeTs = new Timesheet()
            {

                MissionId = Timesheet.MissionId,
                Time = Timesheet.Time,
                DateVolunteered = Timesheet.DateVolunteered,
                Notes = Timesheet.Notes,
                Action = Timesheet.Action,
                UserId = Timesheet.UserId,
                CreatedAt = DateTime.Now,
            };
            if (Timesheet.DateVolunteered >= missionobj.StartDate && Timesheet.DateVolunteered <= DateTime.Now )
            {
                _unitOfWork.Timesheet.Add(timeTs);
                _unitOfWork.Save();
                TimeSheetVM TimeSheetlandingPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
                return PartialView("_TimeSheetView", TimeSheetlandingPageData);
            }
   

            else
            {

                TimeSheetVM TimeSheetlandingPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
                return PartialView("_TimeSheetView", TimeSheetlandingPageData);
            }
        }


        public IActionResult Delete(int id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var data = _unitOfWork.Timesheet.GetFirstOrDefault(e => e.TimesheetId == id);
            _unitOfWork.Timesheet.Remove(data);
            _unitOfWork.Save();
            TimeSheetVM TimeSheetlandingPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
            return PartialView("_TimeSheetView", TimeSheetlandingPageData);
        }

        public JsonResult Edit(int id)
        {
            var data = _unitOfWork.Timesheet.GetFirstOrDefault(e => e.TimesheetId == id);
            return new JsonResult(data);
        }

        [HttpPost]

        public IActionResult Update(Timesheet Timesheet)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var databaseobj = _unitOfWork.Timesheet.GetFirstOrDefault(u => u.TimesheetId == Timesheet.TimesheetId);
            databaseobj.Action = Timesheet.Action;
            databaseobj.DateVolunteered = Timesheet.DateVolunteered;
            databaseobj.Time = Timesheet.Time;
            databaseobj.Notes = Timesheet.Notes;
            databaseobj.UpdatedAt = DateTime.Now;
            //_unitOfWork.Timesheet.Update(databaseobj);
            _unitOfWork.Save();
            TimeSheetVM TimeSheetlandingPageData = _TimeSheetLandingRepository.GetTimeSheetPageData(sessionValue);
            return PartialView("_TimeSheetView", TimeSheetlandingPageData);

        }
    }
}