using CI_platfom.Entity.Models;

namespace CI_platfom.Entity.ViewModel
{
    public class StoryLandingPageVM
    {
        public IEnumerable<Story> Stories { get; set; }
        public IEnumerable<StoryMedium> StoryMedium { get; set; }

        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<MissionTheme> Themes { get; set; }
        public IEnumerable<MissionApplication> missionApplication { get; set; }
        public IEnumerable<StoryInvite> storyInvites { get; set; }
        public IEnumerable<User> UserList { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public Story AppliedStory { get; set; }
        public User LoggedUser { get; set; }
    }
}
