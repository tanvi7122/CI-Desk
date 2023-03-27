﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CI_platfom.Entity.ViewModel
{
    public class ShareYourStoryVM
    {
        [Required]
        public long MissionId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
