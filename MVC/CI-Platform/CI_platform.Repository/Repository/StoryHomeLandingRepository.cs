using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.ViewModel;

namespace CI_platform.Repository.Repository
{
    public class StoryHomeLandingRepository:IStoryHomeLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoryHomeLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public StoryLandingPageVM GetStoryLandingPageData(string email)
        {
            StoryLandingPageVM StoryLandingPageVM = new();
            
            StoryLandingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            StoryLandingPageVM.Countries = _unitOfWork.Country.GetAll();
            StoryLandingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            StoryLandingPageVM.Cities = _unitOfWork.City.GetAll();
            StoryLandingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            StoryLandingPageVM.Skills = _unitOfWork.Skill.GetAll();
            IEnumerable<Story> storiesList;
            storiesList = _unitOfWork.Story.GetStoryCard().Where(u=>u.Status.Equals("PUBLISHED"));
            StoryLandingPageVM.Stories=storiesList;
            return StoryLandingPageVM;
        }
    }
}
