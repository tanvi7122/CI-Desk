using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class StoryMediumRepository : Repository<StoryMedium>, IStoryMediumRepository
    {
        private readonly CiPlatformContext _context;
        public StoryMediumRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
    }
}

