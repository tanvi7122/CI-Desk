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
        public AdminVM GetMissionData(string email)
        {
            AdminVM MissionData = new();
            var loggedId = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            UserData.LoggedUser = loggedId;
            UserData.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            UserData.missionApplications = _unitOfWork.MissionApplication.GetAll().Where(u => u.UserId == loggedId.UserId);
            UserData.Missions = _unitOfWork.Mission.GetAll();
            //TimeSheetPageData.Stories = _unitOfWork.Story.GetAll();

            return UserData;
        }
    }

    }
