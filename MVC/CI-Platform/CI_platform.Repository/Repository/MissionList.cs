using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CI_platform.Repository.Repository
{
    public class MissionList : IMissionList
    {
        private readonly CiPlatformContext _context;

        public MissionList(CiPlatformContext context)
        {
            _context = context;
        }

        public IEnumerable<Mission> GetMissions(List<long> MissionIds)
        {
            var MissionList = _context.Mission.Where(m => MissionIds.Contains(m.MissionId))
                    .Include(m => m.City)
                    .Include(m => m.Country)
                    .Include(m => m.MissionSkills)
                    .ThenInclude(ms => ms.Skill)
                    .Include(m => m.Theme)
                    .Include(m => m.MissionRatings)
                    .Include(m => m.GoalMissions)
                    .Include(m => m.MissionApplications)
                    .Include(m => m.FavouriteMissions)
                    .Include(m => m.MissionMedia).ToList().OrderBy(ml => MissionIds.IndexOf(ml.MissionId));

            return MissionList;


        }


    }
}


