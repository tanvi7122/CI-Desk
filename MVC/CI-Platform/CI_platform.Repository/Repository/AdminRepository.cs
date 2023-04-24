using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
        public class AdminRepository : IAdminRepository
    {
            private readonly IUnitOfWork _unitOfWork;

            public AdminRepository(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public AdminVM GetUserData( string email)
            {
            AdminVM UserData = new();
            UserData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            UserData.Countries= _unitOfWork.Country.GetAll();
            UserData.UserList = _unitOfWork.User.GetAll();
            UserData.Missions = _unitOfWork.Mission.GetAll(); 
                return UserData;
            }

        public AdminVM GetStoryData()
        {
            AdminVM StoryData = new();
     
            StoryData.Stories = _unitOfWork.Story.GetAll();
            StoryData.Missions = _unitOfWork.Mission.GetAll();
            StoryData.UserList = _unitOfWork.User.GetAll();
         
            return StoryData;
        }
   
        public AdminVM ViewStoryData(long id)
        {
            AdminVM StoryData = new();

            StoryData.Stories = _unitOfWork.Story.GetAll();
            StoryData.Missions = _unitOfWork.Mission.GetAll();
            StoryData.UserList = _unitOfWork.User.GetAll();
            StoryData.ViewStory = _unitOfWork.Story.GetFirstOrDefault(Story => Story.StoryId == id);
            return StoryData;
        }
        public AdminVM GetMissionData(string email)
        {
            AdminVM missiondata = new();
            missiondata.Missions = _unitOfWork.Mission.GetAll();
           return missiondata;
        }
        public AdminVM GetCmsData(string email)
        {
            AdminVM CmsData = new();
            CmsData.CmsPage = _unitOfWork.CmsPage.GetAll();
      
            return CmsData;
        }
        public AdminVM GetMissionApplicationsData()
        {
            AdminVM MissionApplicationsData = new();
            MissionApplicationsData.missionApplications = _unitOfWork.MissionApplication.GetAll();
            MissionApplicationsData.Missions = _unitOfWork.Mission.GetAll();
            MissionApplicationsData.UserList = _unitOfWork.User.GetAll();
            return MissionApplicationsData;
        }
        public AdminVM GetMissionSkill()
        { 
            AdminVM SkillData = new();
            SkillData.missionSkills= _unitOfWork.MissionSkill.GetAll();
            SkillData.Skill= _unitOfWork.Skill.GetAll();
            SkillData.Missions = _unitOfWork.Mission.GetAll();
            return SkillData;
        }
        public AdminVM GetMissionThemes()
        {
            AdminVM MissionThemesData = new();
            MissionThemesData.missionThemes = _unitOfWork.MissionTheme.GetAll();
            MissionThemesData.Missions = _unitOfWork.Mission.GetAll();
            return MissionThemesData;
        }

    }

}
