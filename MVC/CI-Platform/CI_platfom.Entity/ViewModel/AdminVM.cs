using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class AdminVM:HeaderVM
    {
        public IEnumerable<Mission> Missions { get; set; } = new List<Mission>();

        public IEnumerable<Story> Stories { get; set; } = new List<Story>();

        public IEnumerable<MissionInvite> MissionInvites { get; set; } = new List<MissionInvite>();

        public IEnumerable<User> UserList { get; set; } = new List<User>();

      
        public IEnumerable<MissionApplication> missionApplications { get; set; } = new List<MissionApplication>();
    }
}
