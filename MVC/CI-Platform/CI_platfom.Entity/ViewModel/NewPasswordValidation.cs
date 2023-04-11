using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class NewPasswordValidation: HeaderVM
    {
        [Required]
        public string? Password { get; set; }
    }
}


