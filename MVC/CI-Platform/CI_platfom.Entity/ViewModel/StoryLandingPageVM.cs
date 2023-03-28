using CI_platfom.Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_platfom.Entity.ViewModel
{
    public class  StoryLandingPageVM
    {
        [Required]
        public long MissionId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long UserId { get; set; }
    
        public string? MediaName { get; set; }

        public string? MediaType { get; set; }

        public string? MediaPath { get; set; }

        public int? Default { get; set; }

        public DateTime? CreatedAt { get; set; }
        public IEnumerable<Story> Stories { get; set; }
        public IEnumerable<StoryMedium> StoryMedium { get; set; }
        public IEnumerable<Mission> Mission { get; set; }
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
