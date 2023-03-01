using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.Models
{
    public class RegisterView
    {
        public User User { get; set; }
  
        public string ConfirmPassword { get; set; }
    }
}
