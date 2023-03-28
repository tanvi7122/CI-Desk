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
        

    }
}
