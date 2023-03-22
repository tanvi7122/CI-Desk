using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class StoryInviteRepository:Repository<StoryInvite>,IStoryInviteRepository
    {
        private readonly CiPlatformContext _context;

        public StoryInviteRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
    }
}
