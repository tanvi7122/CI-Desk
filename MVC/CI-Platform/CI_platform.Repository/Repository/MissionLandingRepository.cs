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

            //landingPageVM.Countries = _unitOfWork.Country.GetAll();
            landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            landingPageVM.MissionSkills = _unitOfWork.MissionSkill.GetAll().Where(m=>m.MissionId==missionId);
            //landingPageVM.MissionMedia = _unitOfWork.MissionMedia.GetAll().Where(m => m.MissionId == missionId);
            IEnumerable<Mission> missionsList;
            //landingPageVM.Cities = _unitOfWork.City.GetAll();
            landingPageVM.RelatedMissions = _unitOfWork.Mission.GetMissionCard().Where(m => (m.ThemeId == themeid || m.CountryId==countryid || m.CityId==cityid) && (m.MissionId!=missionId )).Take(3);
            landingPageVM.MissionMedium = _unitOfWork.MissionMedium.GetAll();
            landingPageVM.MissionInvites = _unitOfWork.MissionInvite.GetAll();
            landingPageVM.missionApplication = _unitOfWork.MissionApplication.GetAll().Where(m => m.MissionId == missionId);
            landingPageVM.missionDocument = _unitOfWork.MissionDocument.GetAll().Where(m => m.MissionId == missionId);
            //landingPageVM.Cities = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined");
            missionsList = _unitOfWork.Mission.GetMissionCardById(missionId);

            landingPageVM.Mission = missionsList;
            landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            int totalrecords = missionsList.Count();


            //int pageSize = 9;
            //missionsList = missionsList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //int totalPages = (int)Math.Ceiling(totalrecords / (double)pageSize);

            //landingPageVM.Missions = missionsList;
            //landingPageVM.CurrentPage = currentPage;
            //landingPageVM.TotalPages = totalPages;
            ////landingPageVM.sort = sort;
            //landingPageVM.PageSize = pageSize;
            //landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            //landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            return landingPageVM;
        }
    }
}
