using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class ForgotPasswordValidation
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

