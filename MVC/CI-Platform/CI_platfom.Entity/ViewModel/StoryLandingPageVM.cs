using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class StoryLandingPageVM
    {
        public IEnumerable<Story> Stories { get; set;}

        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<MissionTheme> Themes { get; set; }

        public IEnumerable<StoryInvite> storyInvites { get; set; }
        public IEnumerable<User> UserList { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public Story AppliedStory { get; set; }
        public User LoggedUser { get; set; }
    }
}
