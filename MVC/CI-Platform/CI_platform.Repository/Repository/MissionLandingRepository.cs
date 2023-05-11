using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.ViewModel;

namespace CI_platform.Repository.Repository
{
    public class MissionLandingRepository:IMissionLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MissionLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public HomeLandingPageVM GetMissionPageData(string email,long missionId,long themeid,long cityid,long countryid)
        {
            HomeLandingPageVM landingPageVM = new();

            landingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            landingPageVM.AppliedMission = _unitOfWork.Mission.GetFirstOrDefault(u => u.MissionId == missionId);
            landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            landingPageVM.MissionSkills = _unitOfWork.MissionSkill.GetAll().Where(m=>m.MissionId==missionId);
            IEnumerable<Mission> missionsList;
            landingPageVM.RelatedMissions = _unitOfWork.Mission.GetMissionCard().Where(m => (m.ThemeId == themeid || m.CountryId==countryid || m.CityId==cityid) && (m.MissionId!=missionId )).Take(3);
            landingPageVM.MissionMedium = _unitOfWork.MissionMedium.GetAll();
            landingPageVM.MissionInvites = _unitOfWork.MissionInvite.GetAll();
            landingPageVM.missionApplication = _unitOfWork.MissionApplication.GetAll().Where(m => m.MissionId == missionId);
            landingPageVM.missionDocument = _unitOfWork.MissionDocument.GetAll().Where(m => m.MissionId == missionId);
            missionsList = _unitOfWork.Mission.GetMissionCardById(missionId);
            landingPageVM.Mission = missionsList;
            landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            int totalrecords = missionsList.Count();
            return landingPageVM;
        }
    }
}
