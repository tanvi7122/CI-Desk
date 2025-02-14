﻿using CI_platform.Repository.Interface;
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

            storylandingPageVM.AppliedStory = _unitOfWork.Story.GetStoryCardById(storyId);
            storylandingPageVM.storyInvites = _unitOfWork.StoryInvite.GetAll();
            storylandingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            //IEnumerable<Story> storylist;
            //storylist = _unitOfWork.Story.GetStoryCardById(storyId);
            storylandingPageVM.AppliedStory.Mission = _unitOfWork.Mission.GetFirstOrDefault(u => u.MissionId == missionId);
            //storylandingPageVM.Stories = storylist;
            storylandingPageVM.Skills = _unitOfWork.Skill.GetAll();
            storylandingPageVM.Cities = _unitOfWork.City.GetAll();
            storylandingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            storylandingPageVM.Countries = _unitOfWork.Country.GetAll();
            storylandingPageVM.StoryMedium = _unitOfWork.storyMedium.GetAll();
            return storylandingPageVM;
        }

      
        public StoryLandingPageVM GetPreviewStoryPageData(long userId, long missionId)
        {
            StoryLandingPageVM storylandingPageVM = new();

            storylandingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
            storylandingPageVM.AppliedStory = _unitOfWork.Story.GetPreviewStoryCardById(userId, missionId);
            storylandingPageVM.storyInvites = _unitOfWork.StoryInvite.GetAll();
            storylandingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.UserId != userId);
            storylandingPageVM.Skills = _unitOfWork.Skill.GetAll();
            storylandingPageVM.Cities = _unitOfWork.City.GetAll();
            storylandingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            storylandingPageVM.Countries = _unitOfWork.Country.GetAll();
            return storylandingPageVM;
        }
    }
}
