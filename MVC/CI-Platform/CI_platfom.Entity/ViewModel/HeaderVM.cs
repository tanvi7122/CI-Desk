using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class HeaderVM
    {
        public User? LoggedUser { get; set; }
        public ContactU? Contact { get; set; }
    }
}
