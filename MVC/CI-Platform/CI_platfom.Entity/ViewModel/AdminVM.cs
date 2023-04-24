using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class AdminVM : HeaderVM
    {
        [Required(ErrorMessage = "this field is required")]
        public CmsPage? AddCmsPage { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public Mission Mission { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public User? Profile { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public Story ViewStory { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public MissionMedium MissionMedium { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public Skill skill { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public MissionTheme missionTheme { get; set; }
        
        public IEnumerable<MissionMedium> missionMedia { get; set; }=new List<MissionMedium>();
        public IEnumerable <Country> Countries { get; set; }= new List<Country>();
        public IEnumerable<Mission> Missions { get; set; } = new List<Mission>();

        public IEnumerable<Story> Stories { get; set; } = new List<Story>();

        public IEnumerable<MissionInvite> MissionInvites { get; set; } = new List<MissionInvite>();

        public IEnumerable<User> UserList { get; set; } = new List<User>();
     public IEnumerable<Skill> Skill { get;set; } = new List<Skill>();
        public IEnumerable<CmsPage> CmsPage { get; set; } = new List<CmsPage>();
        public IEnumerable<MissionSkill> missionSkills { get; set; }=new List<MissionSkill>();
        public IEnumerable<MissionTheme>missionThemes { get; set; }= new List<MissionTheme>();
        public IEnumerable<Banner> banners { get; set; } = new List<Banner>();
        public IEnumerable<MissionApplication> missionApplications { get; set; } = new List<MissionApplication>();
    }
}
