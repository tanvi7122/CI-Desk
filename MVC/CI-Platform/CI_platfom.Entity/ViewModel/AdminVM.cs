﻿using CI_platfom.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class AdminVM : HeaderVM
    {
        public CmsPage AddCmsPage { get; set; }
        public Mission? Mission { get; set; }
        public User? Profile { get; set; }
        public Story? ViewStory { get; set; }
        public MissionMedium? MissionMedium { get; set; }
        public Skill? skill { get; set; }
        public MissionTheme? missionTheme { get; set; }
        public Banner? banner { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public int DefaultSelect { get; set; } = 0;
        public IEnumerable<MissionMedium> missionMedia { get; set; } = new List<MissionMedium>();
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<City> cities { get; set; } = new List<City>();
        public IEnumerable<Mission> Missions { get; set; } = new List<Mission>();
        public IEnumerable<Story> Stories { get; set; } = new List<Story>();
        public IEnumerable<MissionInvite> MissionInvites { get; set; } = new List<MissionInvite>();
        public IEnumerable<User> UserList { get; set; } = new List<User>();
        public IEnumerable<Skill> Skill { get; set; } = new List<Skill>();
        public IEnumerable<CmsPage> CmsPage { get; set; } = new List<CmsPage>();
        public IEnumerable<MissionSkill> missionSkills { get; set; } = new List<MissionSkill>();
        public IEnumerable<MissionTheme> missionThemes { get; set; } = new List<MissionTheme>();
        public IEnumerable<Banner> banners { get; set; } = new List<Banner>();
        public IEnumerable<MissionApplication> missionApplications { get; set; } = new List<MissionApplication>();
        public Admin Admin { get; set; } = new Admin();
        public IEnumerable<MissionDocument> MissionDocuments { get; set; } = Enumerable.Empty<MissionDocument>();
        public IEnumerable<MissionSkill> MissionSkills { get; set; } = Enumerable.Empty<MissionSkill>();
        public GoalMission? GoalMission { get; set; }


    }

}
