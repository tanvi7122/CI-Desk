using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class AdminUserViewModel
    {
        public List<AdminUser>? users { get; set; }
        public AdminUser? newUser { get; set; }
        public List<CountryModel>? countries { get; set; }

        public List<CityModel>? cities { get; set; }
    }
    public class CountryModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }

    }
    public class CityModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? CountryId { get; set; }
    }


    public class AdminUser
    {
        public long userId { get; set; }

        [Required(ErrorMessage = "Please enter a valid first name.")]
        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public string? email { get; set; }

        public string password { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        [Range(0, long.MaxValue, ErrorMessage = "Please enter a positive integer.")]
        public long PhoneNumber { get; set; }

        public long? cityId { get; set; }

        public long? countryId { get; set; }

        public string? employeeId { get; set; }

        public string? department { get; set; }

        public int status { get; set; }
    }
}
