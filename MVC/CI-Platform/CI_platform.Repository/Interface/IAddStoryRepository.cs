using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IAddStoryRepository
    {
        void Add(StoryLandingPageVM ShareStory);
        void save();
    }
}
