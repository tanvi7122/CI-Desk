using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI_platfom.Entity.Models;

public partial class User
{
    public long UserId { get; set; }

    [Required(ErrorMessage = "Please enter your first name.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Please enter your last name.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Please enter your email address.")]
    public string Email { get; set; } = null!;
   
    [Required(ErrorMessage = "Please enter a password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Please enter your phone number.")]
    public long PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public string? WhyIVolunteer { get; set; }

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public long? CityId { get; set; }

    public long? CountryId { get; set; }

    public string? ProfileText { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? Title { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<ContactU> ContactUs { get; } = new List<ContactU>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionInvite> MissionInviteFromUsers { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionInvite> MissionInviteToUsers { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    public virtual ICollection<StoryInvite> StoryInviteFromUsers { get; } = new List<StoryInvite>();

    public virtual ICollection<StoryInvite> StoryInviteToUsers { get; } = new List<StoryInvite>();

    public virtual ICollection<Timesheet> Timesheets { get; } = new List<Timesheet>();

    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
