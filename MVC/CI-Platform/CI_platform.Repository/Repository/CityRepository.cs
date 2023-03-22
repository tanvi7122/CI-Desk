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
   public class CityRepository: Repository<City>, ICityRepository
    {
        private readonly CiPlatformContext _context;

        public CityRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
    }
}
