using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPlatform.Entities.Models;
using CIPlatform.Repository;
using System.Linq.Expressions;


namespace CIPlatform.Repository.Interface
{
    public interface IAccountRepository
    {

        void Add(User user);
        void save();
        public User GetUserEmail(string email);
    }
}