using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class DropDown: HeaderVM
    {
        [DisplayName("Country")]
        public long CountryId { get; set; }
        public string Name { get; set; } = null!;
        public long CityId { get; set; }


    }
}
