using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using CI_platform.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class MissionRepository : Repository<Mission>,IMissionRepository
    {
        private readonly CiPlatformContext _context;
        public MissionRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Mission> GetMissionCard()
        {
            IEnumerable<Mission> missions = _context.Missions.Include(m => m.City).Include(m => m.Theme)
                .Include(m => m.GoalMissions).Include(m => m.MissionApplications)
                .Include(m => m.MissionRatings).Include(m => m.MissionMedia).Include(m => m.MissionSkills).Include(m => m.MissionInvites).
                Include(m => m.FavouriteMissions);
            return missions;
        }
        public IEnumerable<Mission> GetMissionCardById(long id)
        {
            IEnumerable<Mission> missionBYId = _context.Missions.Include(m => m.City).Include(m => m.Theme)
                .Include(m => m.GoalMissions).Include(m => m.MissionApplications)
                .Include(m => m.MissionRatings).Include(m => m.MissionMedia).Include(m => m.MissionSkills).Include(m => m.MissionInvites).
                Include(m => m.FavouriteMissions).Include(m => m.Comments).Include(m => m.MissionDocuments).Where(m => m.MissionId == id);

            return missionBYId;
        }

        //    public IEnumerable<Mission> GetMissionSortNew()
        //    {
        //        var missionSortNew=_context.Missions.OrderBy<Mission, CreatedAt>
        //    }
    }
}
