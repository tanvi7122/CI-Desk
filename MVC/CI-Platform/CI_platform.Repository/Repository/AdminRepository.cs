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
            public AdminVM GetUserData(string email)
            {
            AdminVM UserData = new();
                var loggedId = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            UserData.LoggedUser = loggedId;
            UserData.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            UserData.missionApplications = _unitOfWork.MissionApplication.GetAll().Where(u => u.UserId == loggedId.UserId);
            UserData.Missions = _unitOfWork.Mission.GetAll();
                //TimeSheetPageData.Stories = _unitOfWork.Story.GetAll();

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
        public AdminVM GetMissionData()
        {
            AdminVM missiondata = new();
            missiondata.Missions = _unitOfWork.Mission.GetAll();
           return missiondata;
        }
        public AdminVM GetCmsData()
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


    }

}
