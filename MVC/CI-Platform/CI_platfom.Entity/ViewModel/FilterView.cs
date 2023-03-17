using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class FilterView
    { 
        public IEnumerable<Landing_page> Landing_page { get; set; }
        public long Theme_id { get; set; }
        public string? Title { get; set; }

        public long Country_id { get; set; }

        public string? Country { get; set; }

        public string? CityName { get; set; }
        public long SkillId { get; set; }

        public string? SkillName { get; set; }
        public long CityId { get; set; }
    }
}
