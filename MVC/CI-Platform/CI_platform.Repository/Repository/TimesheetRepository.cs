﻿using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
  
        public class TimesheetRepository : Repository<Timesheet>, ITimesheetRepository
        {
            private readonly CiPlatformContext _context;
            public TimesheetRepository(CiPlatformContext context) : base(context)
            {
                _context = context;
            }
        }
    
}
