using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;

namespace CI_platform.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public readonly CiPlatformContext _CIPlatformContext;

        public AccountRepository(CiPlatformContext CIPlatformContext)
        {
            _CIPlatformContext = CIPlatformContext;

        }
        public void Add(User User)
        {
            _CIPlatformContext.Add(User);
        }
        public void save()
        {
            _CIPlatformContext.SaveChanges();
        }
        public User GetUserEmail(string email)
        {
            return _CIPlatformContext.Users.FirstOrDefault(x => x.Email == email);
        }
        public void UpdateUser(NewPasswordValidation obj)
        {
            PasswordReset passwordresetlast = _CIPlatformContext.PasswordResets.OrderBy(i => i.Id).Last();
            User newvalue = _CIPlatformContext.Users.FirstOrDefault(u => u.Email == passwordresetlast.Email);
            newvalue.Password = obj.Password;
            newvalue.UpdatedAt = DateTime.Now;
            _CIPlatformContext.Update(newvalue);
            _CIPlatformContext.SaveChanges();
        }
    }
}