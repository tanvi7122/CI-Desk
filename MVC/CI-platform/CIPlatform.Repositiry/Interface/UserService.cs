using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClPlatform.entities.Data;
using ClPlatform.Models;


namespace CIPlatform.Repository.Interface
{
   
    namespace YourProject.Services
    {
        public class UserService
        {
            private readonly YourDbContext _dbContext;

            public UserService(YourDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public void RegisterUser(User user)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
        }
    }

}
