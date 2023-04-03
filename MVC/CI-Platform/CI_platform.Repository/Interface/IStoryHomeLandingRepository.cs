using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IStoryHomeLandingRepository
    {

        StoryLandingPageVM GetStoryLandingPageData(string email, int currentPage);

        public Task<bool> ShareYourStory(long id,StoryAdd newstory, List<IFormFile> Photos);
    }
}
