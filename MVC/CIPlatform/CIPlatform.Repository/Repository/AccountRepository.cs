using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    { 
    public readonly CIPlatformContext _CIPlatformContext;

        public AccountRepository(CIPlatformContext CIPlatformContext)
        {
            _CIPlatformContext = CIPlatformContext;

        }
        public void Add(User user)
    {
        _CIPlatformContext.Add(user);
    }
        public void save()
    {
        _CIPlatformContext.SaveChanges();
    }
        public User GetUserEmail(string email)
        {
            return _CIPlatformContext.Users.FirstOrDefault(x => x.Email == email);
        }



    }
}