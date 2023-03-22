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
    public class FavouriteMissionRepository : Repository<FavouriteMission>,IFavouriteMissionRepository
    {
        private readonly CiPlatformContext _context;

        public FavouriteMissionRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
    }
}
