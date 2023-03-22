using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.Data;

namespace CI_platform.Repository.Repository
{
    public class MissionInviteRepository:Repository<MissionInvite>,IMissionInviteRepository
    {
        private readonly CiPlatformContext _context;

        public MissionInviteRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
        //public void Update(MissionInvite missionInvite)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdatePassword(MissionInvite missionInvite)
        //{
        //    missionInvite.DeletedAt = DateTime.Now;
        //    _context.MissionInvites.Update(missionInvite);
        //}
    }
}
