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
    public class HomeLandingRepository:IHomeLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HomeLandingPageVM GetUserProfileData(string email)
        {
            HomeLandingPageVM UserProfileDataVM = new();
            
            UserProfileDataVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            UserProfileDataVM.UserProfile=_unitOfWork.User.GetFirstOrDefault(x => x.Email == email);
            UserProfileDataVM.Countries = _unitOfWork.Country.GetAll();
            UserProfileDataVM.Skills = _unitOfWork.Skill.GetAll();
            return UserProfileDataVM;
        
        }
        public HomeLandingPageVM GetLandingPageData(string email, int currentPage)
        {
            HomeLandingPageVM landingPageVM = new();

            landingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            landingPageVM.Countries = _unitOfWork.Country.GetAll();
            landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            landingPageVM.MissionMedium = _unitOfWork.MissionMedium.GetAll();
            IEnumerable<Mission> missionsList;
            landingPageVM.Cities = _unitOfWork.City.GetAll();
            //missionsList = _unitOfWork.Mission.GetMisssionCard();
            landingPageVM.MissionInvites = _unitOfWork.MissionInvite.GetAll();
            //landingPageVM.Cities = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined");
            missionsList = _unitOfWork.Mission.GetMissionCard();
            int totalrecords = missionsList.Count();
            int pageSize = 9;
            missionsList = missionsList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling(totalrecords / (double)pageSize);

            landingPageVM.Mission = missionsList;
            landingPageVM.CurrentPage = currentPage;
            landingPageVM.TotalPages = totalPages;
            //landingPageVM.sort = sort;
            //landingPageVM.PageSize = pageSize;
            landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            return landingPageVM;
        }
    }
}
