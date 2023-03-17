using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace CI_platform.Repository.Repository
{
    public class MissionDetail : IMissionDetail
    {
        private readonly CiPlatformContext _context;

        public MissionDetail(CiPlatformContext context)
        {
            _context = context;
        }

        public Mission GetMissionDetails(long MissionId)
        {
            Mission mission = _context.Mission.Include(m => m.Country).
             Include(m => m.City).
             Include(m => m.MissionRatings).
             Include(m => m.Theme).
            Include(m => m.MissionSkills).
            ThenInclude(m => m.Skill).
            Include(m => m.MissionApplications).
            Include(m => m.GoalMissions).
            Include(m => m.FavouriteMissions).
            Include(m => m.MissionMedia).
            Include(m => m.Comments).
            ThenInclude(u => u.User).
            FirstOrDefault(m => m.MissionId == MissionId);

            return mission;
        }
    }
}


