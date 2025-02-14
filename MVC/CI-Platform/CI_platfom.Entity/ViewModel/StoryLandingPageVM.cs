﻿using CI_platfom.Entity.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CI_platfom.Entity.ViewModel
{
    public class  StoryLandingPageVM: HeaderVM
    {
        public long MissionId { get; set; }
        public string? Title { get; set; }
        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public IEnumerable<Story>? Stories { get; set; }
        public IEnumerable<StoryMedium>? StoryMedium { get; set; }
        public IEnumerable<Mission>? Mission { get; set; }
        public IEnumerable<City>? Cities { get; set; }
        public IEnumerable<Country>? Countries { get; set; }
        public IEnumerable<Timesheet>? Timesheets { get; set; }

        public IEnumerable<MissionTheme>? Themes { get; set; }
        public IEnumerable<MissionApplication>? missionApplication { get; set; }
        public IEnumerable<StoryInvite>? storyInvites { get; set; }
        public IEnumerable<User>? UserList { get; set; }
        public IEnumerable<Skill>? Skills { get; set; }
        public Story? AppliedStory { get; set; }
        public IEnumerable<StoryMedium>? storyMedia { get; set; }
     
        public IFormFile? FormFile { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalStory { get; set; }
        public int TotalPages { get; set; }
    }
}
