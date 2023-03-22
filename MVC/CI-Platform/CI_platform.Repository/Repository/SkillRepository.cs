﻿using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class SkillRepository:Repository<Skill>,ISkillRepository
    {
        private readonly CiPlatformContext _context;
        public SkillRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
    }
}
