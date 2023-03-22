
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
    public class UserRepository: Repository<User>, IUserRepository
    {
        private readonly CiPlatformContext _context;

        public UserRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Update(user);
        }
    }
}
