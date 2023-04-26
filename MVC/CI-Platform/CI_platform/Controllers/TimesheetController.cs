
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "user")]
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
        [Authorize(Roles = "user")]
        public IActionResult AddTimeTs(Timesheet Timesheet)

        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            //TimeOnly ts = Timesheet.Time.TimeOfDay;
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