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
    public class StoryLandingRepository : IStoryLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoryLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public StoryLandingPageVM GetStoryPageData(string email, long storyId, long missionId)
        {
            StoryLandingPageVM storylandingPageVM = new();

            storylandingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);

            storylandingPageVM.AppliedStory = _unitOfWork.Story.GetFirstOrDefault(u => u.StoryId == storyId);
            storylandingPageVM.storyInvites = _unitOfWork.StoryInvite.GetAll();
            storylandingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            storylandingPageVM.Mission = _unitOfWork.Mission.GetAll();
            storylandingPageVM.StoryMedium = _unitOfWork.storyMedium.GetAll();
            IEnumerable<Story> storylist;
            storylist = _unitOfWork.Story.GetStoryCardById(storyId);
            storylandingPageVM.AppliedStory.Mission = (Mission)_unitOfWork.Mission.GetMissionCard(/*u => u.MissionId == missionId*/);
            storylandingPageVM.Stories = storylist;
            storylandingPageVM.Skills = _unitOfWork.Skill.GetAll();
            storylandingPageVM.Cities = _unitOfWork.City.GetAll();
            storylandingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            storylandingPageVM.Countries = _unitOfWork.Country.GetAll();
            return storylandingPageVM;
        }
    }
}
