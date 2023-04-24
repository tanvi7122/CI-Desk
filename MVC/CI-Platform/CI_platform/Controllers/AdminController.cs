using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class AdminController : Controller
    {
      
            private readonly IUnitOfWork _unitOfWork;
        
            private readonly IHomeLandingRepository _HomeLandingRepository;
            private readonly IMissionLandingRepository _MissionLandingRepository;
             private readonly IAdminRepository _AdminRepository;
            private readonly IConfiguration _config;

            public AdminController( IConfiguration config, IUnitOfWork unitOfWork, IHomeLandingRepository HomeLandingRepository, IAdminRepository AdminRepository,IMissionLandingRepository MissionLandingRepository)
            {
                _unitOfWork = unitOfWork;

               
                _HomeLandingRepository = HomeLandingRepository;
                _config = config;
                _MissionLandingRepository = MissionLandingRepository;
            _AdminRepository = AdminRepository;

            }
        public IActionResult Admin()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            return PartialView("_AdminLayout");
        }

        //-----Admin User----
        public IActionResult admin_user()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM UserData = _AdminRepository.GetUserData(sessionValue);
            return View(UserData);
        }
        public IActionResult AdminAddUser(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM UserData = _AdminRepository.GetUserData(sessionValue);
            if (Id > 0)
            {
                UserData.Profile = _unitOfWork.User.GetFirstOrDefault(User => User.UserId == Id);
            }
            return PartialView("_AdminAddUserPartial", UserData);
        }

        [HttpPost]
        public IActionResult AdminAddUser(AdminVM userProfile)
        {
            try
            {
                // Update the user profile in the database
                User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userProfile.Profile.UserId);
                if (user != null)
                {
                    user.Department = userProfile.Profile.Department;
                    user.Email = userProfile.Profile.Email;
                    user.PhoneNumber = userProfile.Profile.PhoneNumber;
                    user.Status = userProfile.Profile.Status;
                    user.Title = userProfile.Profile.Title;
                    user.CityId = userProfile.Profile.CityId;
                    user.CountryId = userProfile.Profile.CountryId;
                    user.FirstName = userProfile.Profile.FirstName;
                    user.LastName = userProfile.Profile.LastName;
                    user.EmployeeId = userProfile.Profile.EmployeeId;
                    _unitOfWork.Save();
                }
                else
                {
                    userProfile.Profile.UpdatedAt = DateTime.Now;
                    userProfile.Profile.Password = "qwertyuiop";
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(userProfile.Profile.Password);
                    userProfile.Profile.Password = passwordHash;
                    _unitOfWork.User.Add(userProfile.Profile);
                    _unitOfWork.Save();
                }
                // Return a success response
                return RedirectToAction("admin_user");
            }
            catch (Exception ex)
            {
                // Handle the exception and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        public IActionResult DeleteUser(int UserId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var user_deleted = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == UserId);
            user_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            return RedirectToAction("admin_user", "Admin");
        }


        //----Admin StoryPage
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
        public IActionResult ApproveStatusStory(long Id, bool status)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Story_status = _unitOfWork.Story.GetFirstOrDefault(Story => Story.StoryId == Id);
            if (status)
            {
                Story_status.Status = "PUBLISHED";

            }
            else
            {
                Story_status.Status = "DECLINED";
            }
            _unitOfWork.Save();
            return RedirectToAction("admin_story", "Admin");
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
            var story_deleted = _unitOfWork.Story.GetFirstOrDefault(s => s.StoryId == Id);
            story_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM StoryData = _AdminRepository.GetStoryData();
            return RedirectToAction("admin_story", "Admin");
        }
        public IActionResult AdminViewStory(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            AdminVM StoryData = _AdminRepository.ViewStoryData(Id);
            return PartialView("_AdminViewStory", StoryData);

        }
      

        //----Admin Csm Page
        public IActionResult admin_cms()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM CmsData = _AdminRepository.GetCmsData(sessionValue);
            return View(CmsData);
        }
        public IActionResult DeleteCms(int Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var cms_deleted = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == Id);/*GetAll().Where(u => u.UserId == userId);*/
            cms_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM CmsData = _AdminRepository.GetCmsData(sessionValue);
            return RedirectToAction("admin_cms", "Admin");
        }
        public IActionResult AdminAddCms(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            AdminVM CmsData = _AdminRepository.GetCmsData(sessionValue);
            if (Id > 0)
            {
                CmsData.AddCmsPage = _unitOfWork.CmsPage.GetFirstOrDefault(CmsPage => CmsPage.CmsPageId == Id);
            }
            return PartialView("AdminAddCms", CmsData);
        }
        [HttpPost]
        public IActionResult AdminAddCms(AdminVM AddCms)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var cms = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == AddCms.AddCmsPage.CmsPageId);
            if (AddCms != null)
            {
                cms.Title = AddCms.AddCmsPage.Title;
                cms.Description = AddCms.AddCmsPage.Description;
                cms.Slug = AddCms.AddCmsPage.Slug;
                cms.Status = AddCms.AddCmsPage.Status;
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.CmsPage.Add(AddCms.AddCmsPage);
                _unitOfWork.Save();
            }
            return RedirectToAction("admin_cms", "Admin");

        }
        //---Admin Mission Applicaton
        public IActionResult admin_mission_application()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            
            AdminVM MissionApplicationsData = _AdminRepository.GetMissionApplicationsData();
            return View(MissionApplicationsData);
        }
        public IActionResult ApproveStatusMissionApplication(long Id, bool status)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Mission_Application_deleted = _unitOfWork.MissionApplication.GetFirstOrDefault(MissionApplication => MissionApplication.MissionApplicationId == Id);
            if (status)
            {
                Mission_Application_deleted.ApprovalStatus = "APPROVE";//DECLINE

            }
            else
            {
                Mission_Application_deleted.ApprovalStatus = "DECLINE";
            }
            _unitOfWork.Save();
            return RedirectToAction("admin_mission_application", "Admin");
        }
        public IActionResult Delete_Admin_MissionApplication(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Application_deleted = _unitOfWork.MissionApplication.GetFirstOrDefault(MissionApplication => MissionApplication.MissionApplicationId == Id);
            Application_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();

            return RedirectToAction("admin_mission_application", "Admin");
        }
        //---Admin MissionTheme
        public IActionResult Admin_MissionTheme()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM Missionthemedata = _AdminRepository.GetMissionThemes();
            return View(Missionthemedata);
        }
        public IActionResult UpdateMissionTheme(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM MissionThemesData = _AdminRepository.GetMissionThemes();
            if (Id > 0)
            {
                MissionThemesData.missionTheme = _unitOfWork.MissionTheme.GetFirstOrDefault(MissionTheme => MissionTheme.MissionThemeId == Id);
            }
            return PartialView("UpdateMissionTheme", MissionThemesData);
        }
        [HttpPost]
        public IActionResult UpdateMissionTheme(AdminVM adminmissionTheme)
        {
            var MissionTheme = _unitOfWork.MissionTheme.GetFirstOrDefault(MissionTheme => MissionTheme.MissionThemeId == adminmissionTheme.missionTheme.MissionThemeId);
            if (MissionTheme != null)
            {
                MissionTheme.Title = adminmissionTheme.missionTheme.Title;
                MissionTheme.Status = adminmissionTheme.missionTheme.Status;
            }
            else
            {
                _unitOfWork.MissionTheme.Add(adminmissionTheme.missionTheme);
                _unitOfWork.Save();
            }
            return RedirectToAction("Admin_MissionTheme");
        }
        public IActionResult DeletemissionThemes(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Application_deleted = _unitOfWork.MissionTheme.GetFirstOrDefault(MissionTheme => MissionTheme.MissionThemeId == Id);/*GetAll().Where(u => u.UserId == userId);*/
            Application_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
         
            return RedirectToAction("Admin_MissionTheme", "Admin");
        }

        //!---Admin SkillPage
        public IActionResult Admin_Mission_Skill()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM MissionSkillData = _AdminRepository.GetMissionSkill();
            return View(MissionSkillData);
        }
      
             public IActionResult DeleteSkill(int Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Skill_deleted = _unitOfWork.Skill.GetFirstOrDefault(Skill=>Skill.SkillId == Id);
            Skill_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            return RedirectToAction("admin_Mission_Skill", "Admin");
        }

        public IActionResult AdminSkill(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM SkillData = _AdminRepository.GetMissionSkill();
            if (Id > 0)
            {
                SkillData.skill = _unitOfWork.Skill.GetFirstOrDefault(Skill => Skill.SkillId == Id);
            }
            return PartialView("_AdminAddSkillPartial", SkillData);
        }
        [HttpPost]
        public IActionResult AdminSkill(AdminVM adminskills)
        {
            var Addskill = _unitOfWork.Skill.GetFirstOrDefault(Skill => Skill.SkillId == adminskills.skill.SkillId);
            if (Addskill != null)
            {
                Addskill.SkillName = adminskills.skill.SkillName;
                Addskill.Status = adminskills.skill.Status;
            }
            else
            {
                _unitOfWork.Skill.Add(adminskills.skill);
                _unitOfWork.Save();
            }
            return RedirectToAction("Admin_Mission_Skill");
        }

        //---Admin Mission
        public IActionResult admin_mission()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM missiondata = _AdminRepository.GetMissionData(sessionValue);
            return View(missiondata);

        }

        public IActionResult DeleteMission(int Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");


            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            var Mission_deleted = _unitOfWork.Mission.GetFirstOrDefault(m=>m.MissionId == Id);
            Mission_deleted.DeletedAt = DateTime.Now;
            _unitOfWork.Save();
            AdminVM MissionData = _AdminRepository.GetMissionData(sessionValue);
            return RedirectToAction("admin_mission", "Admin");
        }

        public IActionResult AdminAddMission(long Id)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");

            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            AdminVM MissionData = _AdminRepository.GetMissionData(sessionValue);
            if (Id > 0)
            {
                MissionData.Mission = _unitOfWork.Mission.GetFirstOrDefault(Mission => Mission.MissionId == Id);
            }
            return View("AdminAddMission");
        }

        [HttpPost]
        public IActionResult AdminAddMission(AdminVM mission)
        {
            try
            {
                // Update the user profile in the database
                Mission Addmission = _unitOfWork.Mission.GetFirstOrDefault(Mission => Mission.MissionId == mission.Mission.MissionId);
                if (Addmission != null)
                {
                    Addmission.Title = mission.Mission.Title;
                    Addmission.ShortDescription = mission.Mission.ShortDescription;
                    Addmission.Description = mission.Mission.Description;
                    Addmission.MissionType = mission.Mission.MissionType;
                    Addmission.Status = mission.Mission.Status;
                    Addmission.OrganizationName = mission.Mission.OrganizationName;
                    Addmission.OrganizationDetail = mission.Mission.OrganizationDetail;
                    Addmission.Availability = mission.Mission.Availability;
                    Addmission.CityId = mission.Mission.CityId;
                    Addmission.ThemeId = mission.Mission.ThemeId;
                    Addmission.CountryId = mission.Mission.CountryId;
                   Addmission.StartDate = mission.Mission.StartDate;
                    Addmission.EndDate = mission.Mission.EndDate;
                    _unitOfWork.Save();
                }
                else
                {
                    mission.Mission.UpdatedAt = DateTime.Now;
                    _unitOfWork.Mission.Add(mission.Mission);
                    _unitOfWork.Save();
                }
                // Return a success response
                return RedirectToAction("admin_mission");
            }
            catch (Exception ex)
            {
                // Handle the exception and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}
