using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
 
    public class StoryAdd
    {
        public string MissionTitle { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public List<string> VideoUrl { get; set; }

        public int value { get; set; }

        public List<IFormFile> Photos { get; set; }

    }
}