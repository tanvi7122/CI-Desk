using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
        public interface IMissionList
        {
            public IEnumerable<Mission> GetMissions(List<long> MissionIds);
        }
   
}
