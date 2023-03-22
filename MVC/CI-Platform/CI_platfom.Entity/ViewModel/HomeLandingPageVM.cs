using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
   public class HomeLandingPageVM
    {
        public IEnumerable<MissionMedium> MissionMedium;

        public IEnumerable<Mission> Mission { get; set; }

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<Country> Countries { get; set; }   

        public IEnumerable<MissionTheme> Themes { get; set; }

        //public IEnumerable<MissionApplication> MissionApplications { get; set; }

        public IEnumerable<User> UserList { get; set; }
        
        public IEnumerable<Skill> Skills { get; set; }

        public IEnumerable<MissionSkill> MissionSkills { get; set;}
        public IEnumerable<MissionApplication> missionApplication { get; set; }

        public IEnumerable<MissionDocument> missionDocument { get; set; }
        public IEnumerable<Mission> RelatedMissions { get; set; }
        public IEnumerable<MissionInvite> MissionInvites { get; set; }
        //public IEnumerable<MissionSkills> MissionSkills { get; set; }
        //public string SelectedCountry { get; set; }
        public User LoggedUser { get; set; }
        public Mission AppliedMission { get; set; }
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string sort { get; set; }

    }
}
