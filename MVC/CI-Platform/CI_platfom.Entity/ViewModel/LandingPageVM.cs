using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class LandingPageVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? LastName { get; set; }
        [Required]
       
        public long PhoneNumber { get; set; }
        [Required]
        public string? Avatar { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        public IEnumerable<Banner>? Banners { get; set; }
     

    }
}

