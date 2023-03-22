using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
   
        public interface IUserRepository : IRepository<User>
        {
            void Update(User user);
            void UpdatePassword(User user, string newPassword);
        }
    
}
