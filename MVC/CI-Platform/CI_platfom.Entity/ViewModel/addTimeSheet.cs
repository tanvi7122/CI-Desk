using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class addTimeSheet: HeaderVM
    {
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public DateTime DateVolunteered { get; set; }
        //public int Hour { get; set; }
        //public int minutes { get; set; }
        public string? Notes { get; set; }
        public DateTime Time { get; set; }
        public int? Action { get; set; }


    }
}
