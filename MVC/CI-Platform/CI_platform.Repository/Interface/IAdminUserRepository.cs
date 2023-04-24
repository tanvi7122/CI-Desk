using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IAdminUserRepository
    {
        List<CountryModel> GetCountries();
        List<CityModel> GetCitiesByCountry(long? countryId);
        User AddUser(AdminUser viewModel);
        User EditUser(AdminUser viewModel);
        void removeUser(long userId);
        AdminUser GetUserById(long userId);
        bool UserEmailExist(string email);
     
    }
}
