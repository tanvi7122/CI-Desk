﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class CountryView
    {
        public long CountryId { get; set; }
        public string country { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; } = null!;


    }
}
