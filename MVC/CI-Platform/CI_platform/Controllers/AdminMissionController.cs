using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_platform.Controllers
{
    public class AdminMissionController : Controller
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminRepository _AdminRepository;
        public readonly IWebHostEnvironment _hostEnvironment;
        public AdminMissionController(IUnitOfWork unitOfWork, IAdminRepository AdminRepository, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _AdminRepository = AdminRepository;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "admin")]
        [Route("Admin/Missions")]
        public IActionResult MissionIndex()
        {
            string? sessionValue = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(sessionValue))
            {
               
                return RedirectToAction("Index", "Home");
            }
            AdminVM viewModel = _AdminRepository.GetMissionPageData(sessionValue);
            if (viewModel == null || viewModel.Admin.AdminId == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        public IActionResult GetUpsertMissionPage(long MissionId)
        {
            string? sessionValue = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(sessionValue))
            {
           
                return Content("SessionExpired");
            }
            AdminVM viewModel = _AdminRepository.GetMissionPageData(sessionValue);

            viewModel.Mission = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == MissionId);

            if (viewModel == null )
            {
                return Content("Error");
            }

            if (viewModel.Mission != null)
            {
                viewModel.cities = _unitOfWork.City.GetAll().Where(city => city.CountryId == viewModel.Mission.CountryId);
              
                viewModel.missionMedia = _unitOfWork.MissionMedium.GetAll().Where(media => media.MissionId == MissionId);
                viewModel.MissionDocuments = _unitOfWork.MissionDocument.GetAll().Where(doc => doc.MissionId == MissionId);
                viewModel.MissionSkills = _unitOfWork.MissionSkill.GetAll().Where(skill => skill.MissionId == MissionId);
                if (viewModel.Mission.MissionType == "Goal")
                {
                    viewModel.GoalMission = _unitOfWork.GoalMission.GetFirstOrDefault(goal => goal.MissionId == MissionId);
                }
            }

            return PartialView("_UpsertMission", viewModel);
        }

        //public IActionResult GetCityListMission(long countryId)
        //{
        //    string? sessionValue = HttpContext.Session.GetString("AdminEmail");
        //    if (string.IsNullOrEmpty(sessionValue))
        //    {

        //        return Content("SessionExpired");
        //    }
        //    AdminVM viewModel = _AdminRepository.GetMissionPageData(sessionValue);

        //    if (viewModel == null || viewModel.Admin.AdminId == 0)
        //    {
        //        return Content("Error");
        //    }

        //    viewModel.cities = _unitOfWork.City.GetAll().Where(c => c.CountryId == countryId && c.Name != "Undefined");
        //    return PartialView("_CityListAdmin", viewModel);
        //}


        [HttpPost]
        public IActionResult SaveMission(AdminVM obj, MissionFormAdditionalParams extraParams)
        {
            string? sessionValue = HttpContext.Session.GetString("AdminEmail");
            if (string.IsNullOrEmpty(sessionValue))
            {
               
                return RedirectToAction("Index", "Home");
            }

            if (obj == null || obj.Mission == null)
            {
                return RedirectToAction("Error", "Home");
            }

            Mission MissionObj = obj.Mission;
            string wwwRootPath = _hostEnvironment.WebRootPath;


            if (MissionObj.MissionType == "Goal")
            {
                GoalMission goalMission = new();
                goalMission.GoalValue = extraParams.GoalValue;
                goalMission.GoalObjectiveText = extraParams.GoalText;
                MissionObj.GoalMissions.Add(goalMission);
            }

            if (extraParams.SkillList.Any())
            {
                foreach (var skill in extraParams.SkillList)
                {
                    MissionSkill missionSkill = new();
                    missionSkill.SkillId = skill;
                    MissionObj.MissionSkills.Add(missionSkill);
                }
            }
            IEnumerable<MissionMedium> missionMedia = _unitOfWork.MissionMedium.GetAll().Where(media => media.MissionId == MissionObj.MissionId);
            var dbMedia = missionMedia.Where(m => m.MediaType != "URL");
            if (extraParams.Images.Any())
            {
                foreach (var image in extraParams.Images)
                {
                    var imageName = image.FileName.Substring(0, image.FileName.IndexOf('.'));
                    if (!dbMedia.Any(media => media.MediaName.Contains(imageName)))
                    {
                        MissionMedium media = new();
                        string fileName = "MissionMedia-" + DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                        var uploads = Path.Combine(wwwRootPath, @"images/mission/media");
                        var extension = Path.GetExtension(image.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }
                        media.MediaType = extension.Substring(1);
                        media.MediaPath = @"/images/mission/media/" + fileName + extension;
                        media.MediaName = fileName;
                        //media.Default = extraParams.Images.IndexOf(image) == extraParams.DefaultSelect;
                        MissionObj.MissionMedia.Add(media);
                    }
                }
            }

            foreach (var image in dbMedia)
            {
                if (!extraParams.Images.Any(i => image.MediaName.Contains(i.FileName.Substring(0, i.FileName.IndexOf('.')))))
                {
                    var filepath = wwwRootPath + image.MediaPath;
                    FileInfo file = new FileInfo(filepath);
                    file.Delete();
                    _unitOfWork.MissionMedium.Remove(image);
                }
            }

            var dbURLS = missionMedia.Where(media => media.MediaType == "URL");
            if (!String.IsNullOrWhiteSpace(extraParams.VideoUrl))
            {
                var videoUrlArr = GetAllUrls(extraParams.VideoUrl);

                foreach (var url in videoUrlArr)
                {
                    if (!dbURLS.Any(media => media.MediaPath == url))
                    {
                        MissionMedium media = new();
                        media.MediaType = "URL";
                        media.MediaPath = url;
                        media.MediaName = "Mission Video Url";
                        MissionObj.MissionMedia.Add(media);
                    }
                }

                foreach (var url in dbURLS)
                {
                    if (!videoUrlArr.Any(u => u == url.MediaPath))
                    {
                        _unitOfWork.MissionMedium.Remove(url);
                    }
                }
            }
            else
            {
                if (dbURLS.Any())
                {
                    _unitOfWork.MissionMedium.RemoveRange(dbURLS);
                }
            }
          
            var dbDocs = _unitOfWork.MissionDocument.GetAll().Where(document => document.MissionId == MissionObj.MissionId);
            if (extraParams.Documents.Any())
            {
                foreach (var document in extraParams.Documents)
                {
                    var docName = document.FileName.Substring(0, document.FileName.LastIndexOf('.'));
                    if (!dbDocs.Any(doc => doc.DocumentName.Contains(docName)))
                    {
                        MissionDocument missionDocs = new();
                        string fileName = "MissionDoc-" + DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                        var uploads = Path.Combine(wwwRootPath, @"documents/mission");
                        var extension = Path.GetExtension(document.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            document.CopyTo(fileStream);
                        }
                        missionDocs.DocumentType = extension.ToUpper().Substring(1);
                        missionDocs.DocumentPath = @"/documents/mission/" + fileName + extension;
                        missionDocs.DocumentName = docName;
                        MissionObj.MissionDocuments.Add(missionDocs);
                    }
                }
            }

            foreach (var doc in dbDocs)
            {
                if (!extraParams.Documents.Any(i => doc.DocumentName.Contains(i.FileName.Substring(0, i.FileName.LastIndexOf('.')))))
                {
                    var filepath = wwwRootPath + doc.DocumentPath;
                    FileInfo file = new FileInfo(filepath);
                    file.Delete();
                    _unitOfWork.MissionDocument.Remove(doc);
                }
            }

            _unitOfWork.Save();

            var dbMission = _unitOfWork.Mission.GetFirstOrDefault(Mission => Mission.MissionId== MissionObj.MissionId);
            if (dbMission == null || dbMission.MissionId == 0)
            {
                _unitOfWork.Mission.Add(MissionObj);
               
            }
            else
            {
                dbMission.Title = MissionObj.Title;
                dbMission.ShortDescription = MissionObj.ShortDescription;
                dbMission.Description = MissionObj.Description;
                dbMission.OrganizationName = MissionObj.OrganizationName;
                dbMission.OrganizationDetail = MissionObj.OrganizationDetail;
                dbMission.StartDate = MissionObj.StartDate;
                dbMission.EndDate = MissionObj.EndDate;
                if (dbMission.MissionType == "Time")
                {
                    dbMission.TotalSeats = MissionObj.TotalSeats;
                    //dbMission.Deadline = MissionObj.Deadline;
                }
                else
                {
                    var dbGoal = _unitOfWork.GoalMission.GetFirstOrDefault(goal => goal.MissionId == dbMission.MissionId);
                    if (dbGoal != null)
                    {
                        _unitOfWork.GoalMission.Remove(dbGoal);
                        dbMission.GoalMissions.Clear();
                    }
                    foreach (var goal in MissionObj.GoalMissions)
                    {
                        dbMission.GoalMissions.Add(goal);
                    }
                }
                dbMission.ThemeId = MissionObj.ThemeId;
                foreach (var skill in MissionObj.MissionSkills)
                {
                    if (!dbMission.MissionSkills.Any(s => s.SkillId == skill.SkillId))
                    {
                        dbMission.MissionSkills.Add(skill);
                    }
                }
                var defaultMedia = dbMission.MissionMedia.FirstOrDefault(m => m.Default== 1);
                var newDefaultMedia = extraParams.Images.ElementAt(extraParams.DefaultSelect);
                var newDMName = newDefaultMedia.FileName.Substring(0, newDefaultMedia.FileName.IndexOf('.'));
                if (defaultMedia != null && defaultMedia.MediaName != newDMName)
                {
                    dbMission.MissionMedia.Remove(defaultMedia);
                    defaultMedia.Default = 0;
                    dbMission.MissionMedia.Add(defaultMedia);
                }
                foreach (var media in MissionObj.MissionMedia)
                {
                    dbMission.MissionMedia.Add(media);
                }
                foreach (var doc in MissionObj.MissionDocuments)
                {
                    dbMission.MissionDocuments.Add(doc);
                }
                dbMission.Availability = MissionObj.Availability;
                dbMission.Status = MissionObj.Status;
                dbMission.UpdatedAt = DateTime.Now;
                _unitOfWork.Mission.Add(dbMission);
               
            }
            _unitOfWork.Save();
            return RedirectToAction("MissionIndex");
        }


        [HttpPost]
        public IActionResult DeleteMission(long MissionId)
        {
            string? sessionValue = HttpContext.Session.GetString("AdminEmail");
            if (string.IsNullOrEmpty(sessionValue))
            {
               
                return Content("SessionExpired");
            }
            var dbMissionObj = _unitOfWork.Mission.GetFirstOrDefault(user => user.MissionId == MissionId);
            if (dbMissionObj == null)
            {
                return Content("Invalid Mission");
            }
            dbMissionObj.DeletedAt = DateTime.Now;
            dbMissionObj.Status = 0;
            _unitOfWork.Mission.Add(dbMissionObj);
            _unitOfWork.Save();
       
            return Content("Delete Successful");
        }

        private static string[] GetAllUrls(string str)
        {
            string[] list;

            if (str.Contains(','))
            {
                list = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (str.Contains(' '))
            {
                list = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (str.Contains("\r\n"))
            {
                list = str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                list = str.Split("http", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = "http" + list[i];
                }
            }

            return list;
        }

      
    }
}
