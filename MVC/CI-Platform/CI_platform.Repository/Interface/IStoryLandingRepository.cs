using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IStoryLandingRepository
    {
        StoryLandingPageVM GetStoryPageData(string email, long storyId,long missionId);
        StoryLandingPageVM GetStorySharepage(string email);
    }
}
