using CI_platfom.Entity.Data;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class TimeSheetLandingRepository : ITimeSheetLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeSheetLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TimeSheetVM GetTimeSheetPageData(string email)
        {
            TimeSheetVM TimeSheetPageData = new();
            var loggedId = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            TimeSheetPageData.LoggedUser = loggedId;
            TimeSheetPageData.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            TimeSheetPageData.missionApplications = _unitOfWork.MissionApplication.GetAll().Where(u => u.UserId == loggedId.UserId);
            TimeSheetPageData.timesheets = _unitOfWork.Timesheet.GetAll().Where(u => u.UserId == loggedId.UserId);
            TimeSheetPageData.Missions = _unitOfWork.Mission.GetAll();
            TimeSheetPageData.Stories = _unitOfWork.Story.GetAll();
            TimeSheetPageData.MissionInvites = _unitOfWork.MissionInvite.GetAll();
            TimeSheetPageData.StoryInvites = _unitOfWork.StoryInvite.GetAll();
            return TimeSheetPageData;
        }
    }
}