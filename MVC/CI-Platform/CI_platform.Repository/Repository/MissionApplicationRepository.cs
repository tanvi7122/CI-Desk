using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.Data;
using Microsoft.EntityFrameworkCore;

namespace CI_platform.Repository.Repository
{
    public class MissionApplicationRepository:Repository<MissionApplication>,IMissionApplicationRepository
    {
        private readonly CiPlatformContext _context;

        public MissionApplicationRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<MissionApplication> GetStoryCard()
        {
            var Mission = _context.MissionApplication.Include(m => m.Mission).Include(m => m.User);
            return Mission;
        }
        public IEnumerable<MissionApplication> GetStoryCardById(long id)
        {
            var MissionBYId = _context.MissionApplication.Include(m => m.Mission).Include(m => m.UserId).Include(m => m.MissionApplication).Where(m => m.UserId ==Id);

            return MissionBYId;
        }

    }
}
