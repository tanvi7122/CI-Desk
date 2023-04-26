using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public AdminVM GetUserData(string email)
        {
            AdminVM UserData = new();
            UserData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            UserData.Countries = _unitOfWork.Country.GetAll();
            UserData.UserList = _unitOfWork.User.GetAll();
            UserData.Missions = _unitOfWork.Mission.GetAll();
            return UserData;
        }

        public AdminVM GetStoryData(string email)
        {
            AdminVM StoryData = new();
            StoryData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            StoryData.Stories = _unitOfWork.Story.GetAll();
            StoryData.Missions = _unitOfWork.Mission.GetAll();
            StoryData.UserList = _unitOfWork.User.GetAll();

            return StoryData;
        }

        public AdminVM ViewStoryData(long id, string email)
        {
            AdminVM StoryData = new();
            StoryData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            StoryData.Stories = _unitOfWork.Story.GetAll();
            StoryData.Missions = _unitOfWork.Mission.GetAll();
            StoryData.UserList = _unitOfWork.User.GetAll();
            StoryData.ViewStory = _unitOfWork.Story.GetFirstOrDefault(Story => Story.StoryId == id);
            return StoryData;
        }
        public AdminVM GetMissionData(string email)
        {
            AdminVM missiondata = new();
            missiondata.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            missiondata.Missions = _unitOfWork.Mission.GetAll();
            return missiondata;
        }
        public AdminVM GetCmsData(string email)
        {
            AdminVM CmsData = new();
            CmsData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            CmsData.CmsPage = _unitOfWork.CmsPage.GetAll();
            return CmsData;
        }
        public AdminVM GetMissionApplicationsData(string email)
        {
            AdminVM MissionApplicationsData = new();
            MissionApplicationsData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            MissionApplicationsData.missionApplications = _unitOfWork.MissionApplication.GetAll();
            MissionApplicationsData.Missions = _unitOfWork.Mission.GetAll();
            MissionApplicationsData.UserList = _unitOfWork.User.GetAll();
            return MissionApplicationsData;
        }
        public AdminVM GetMissionSkill(string email)
        {
            AdminVM SkillData = new();
            SkillData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            SkillData.missionSkills = _unitOfWork.MissionSkill.GetAll();
            SkillData.Skill = _unitOfWork.Skill.GetAll();
            SkillData.Missions = _unitOfWork.Mission.GetAll();
            return SkillData;
        }
        public AdminVM GetMissionThemes(string email)
        {
            AdminVM MissionThemesData = new();
            MissionThemesData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            MissionThemesData.missionThemes = _unitOfWork.MissionTheme.GetAll();
            MissionThemesData.Missions = _unitOfWork.Mission.GetAll();
            return MissionThemesData;
        }
        public AdminVM GetBanner(string email)
        {
            AdminVM BannerData = new();
            BannerData.LoggedUser = _unitOfWork.User.GetFirstOrDefault(User => User.Email == email);
            BannerData.banners = _unitOfWork.Banner.GetAll();
            return BannerData;
        }
        public AdminVM GetMissionPageData(string email)
        {
            AdminVM viewModel = new();

            viewModel.LoggedUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == email);
            viewModel.Missions = _unitOfWork.Mission.GetAll().Where(mission => mission.DeletedAt == null);
            viewModel.Countries = _unitOfWork.Country.GetAll().Where(country => country.CountryId > 2);
            viewModel.missionThemes = _unitOfWork.MissionTheme.GetAll();

            viewModel.Skill = _unitOfWork.Skill.GetAll();

            return viewModel;
        }


    }
}
