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
        void Add(User user);
        void save();
        public User GetUserEmail(string email);

        public void UpdateUser(NewPasswordValidation obj);
    }
}


