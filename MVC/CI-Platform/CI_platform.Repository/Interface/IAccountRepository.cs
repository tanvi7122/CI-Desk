using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IAccountRepository
    {
      
        void save();
        public User GetUserEmail(string email);

        public void UpdateUser(NewPasswordValidation obj);
        void Add(CI_platfom.Entity.Models.User user);
    }
}


