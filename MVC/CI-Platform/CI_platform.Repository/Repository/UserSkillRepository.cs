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

    public class UserSkillRepository : Repository<User>, IUserSkillRepository
    {
        private readonly CiPlatformContext _context;

        public UserSkillRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
        public void Add(UserSkill userSkill)
        {
            _context.UserSkills.Add(userSkill);
        }

        public void AddRange(IEnumerable<UserSkill> userSkills)
        {
            _context.UserSkills.AddRange(userSkills);
        }

    }
}
