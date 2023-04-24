using CI_platfom.Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platfom.Entity.ViewModel
{
    public class MissionVm
    {
        public IEnumerable<Country>? countries { get; set; }
        public IEnumerable<Mission>? missions { get; set; }
        public IEnumerable<MissionTheme>? themes { get; set; }
        public List<MissionVm>? missiondetail { get; set; }
        public IEnumerable<Skill>? skills { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "this field is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public DateTime? EndDate { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public long CountryId { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public long CityId { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public long MissionThemeId { get; set; }

        public string MissionType { get; set; } = null!;
        [Required(ErrorMessage = "this field is required")]
        public string? ShortDescription { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string? Description { get; set; }
        public int? Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public string? Availability { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public long? TotalSeats { get; set; }
        [RegularExpression(@"^(http(s)?:\/\/)?((w){3}.)?youtu(be|.be)?(\.com)?\/.+$",
       ErrorMessage = "Please enter a valid YouTube URL.")]
        public string? VideoUrl { get; set; }
        public long SkillId { get; set; }
        public long MissionId { get; set; }

        public string? GoalObjectiveText { get; set; }
        public int GoalValue { get; set; }
        public long[]? SelectedSkills { get; set; } = null;
        public string[]? missionimages { get; set; }
        public string[]? missiondocuments { get; set; }
        public List<IFormFile>? images { get; set; }
        public List<IFormFile>? documents { get; set; }
    }
}
