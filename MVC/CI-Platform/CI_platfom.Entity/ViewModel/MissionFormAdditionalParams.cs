using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
        public class MissionFormAdditionalParams
        {
            public List<long> SkillList { get; set; } = new List<long>();
            public List<IFormFile> Images { get; set; } = new List<IFormFile>();
            public int DefaultSelect { get; set; } = 0;
            public List<IFormFile> Documents { get; set; } = new List<IFormFile>();
            public string VideoUrl { get; set; } = string.Empty;
            public string GoalText { get; set; } = string.Empty;
            public int GoalValue { get; set; } = 0;
        }
    }


