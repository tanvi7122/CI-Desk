using CI_platfom.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class Landing_page
    {
        public string? Availability { get; set; }
        public long MissionId { get; set; }

        public long ThemeId { get; set; }

        public long CountryId { get; set; }

        public long CityId { get; set; }

        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? ThemeTitle { get; set; }
        public string MissionType { get; set; } = null!;
        public string? OrganizationName { get; set; }

        public int Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}