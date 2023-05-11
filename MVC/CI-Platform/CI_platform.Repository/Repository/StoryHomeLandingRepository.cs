using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.ViewModel;
using Microsoft.AspNetCore.Http;

namespace CI_platform.Repository.Repository
{
    public class StoryHomeLandingRepository:IStoryHomeLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoryHomeLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public StoryLandingPageVM GetStoryLandingPageData(string email, int currentPage)
        {
            StoryLandingPageVM StoryLandingPageVM = new();

            StoryLandingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            StoryLandingPageVM.Countries = _unitOfWork.Country.GetAll();
            StoryLandingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
            StoryLandingPageVM.Cities = _unitOfWork.City.GetAll();
            StoryLandingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            StoryLandingPageVM.Skills = _unitOfWork.Skill.GetAll();
            StoryLandingPageVM.missionApplication = _unitOfWork.MissionApplication.GetAll();
            StoryLandingPageVM.Mission = _unitOfWork.Mission.GetAll();
            StoryLandingPageVM.StoryMedium= _unitOfWork.storyMedium.GetAll();
            IEnumerable<Story> storiesList;
            storiesList = _unitOfWork.Story.GetStoryCard().Where(u => u.Status.Equals("PUBLISHED"));
            int totalrecords = storiesList.Count();
            Console.WriteLine(totalrecords);
            int pageSize = 3;
            storiesList = storiesList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling(totalrecords / (double)pageSize);
            StoryLandingPageVM.CurrentPage = currentPage;
            StoryLandingPageVM.TotalPages = totalPages;
            StoryLandingPageVM.TotalStory = totalrecords;
            StoryLandingPageVM.PageSize = pageSize;
            StoryLandingPageVM.Stories = storiesList;
            return StoryLandingPageVM;
        }
        public async Task<bool> ShareYourStory(long id, StoryAdd newstory, List<IFormFile> Photos)
        {
            var value = newstory.value;
            if (value == 1)
            {
                var misssionId = Convert.ToInt64(newstory.MissionTitle);
                var userexist = _unitOfWork.Story.GetFirstOrDefault(s => s.UserId == id && s.MissionId == misssionId);
                if (userexist == null)
                {
                    var story = new Story
                    {
                        UserId = id,
                        MissionId = misssionId,
                        PublishedAt = newstory.Date,
                        Title = newstory.Title,
                        Description = newstory.Description
                    };

                    _unitOfWork.Story.Add(story);
                    _unitOfWork.Save();

                    foreach (var videoUrl in newstory.VideoUrl)
                    {
                        var mediaurl = new StoryMedium
                        {
                            StoryId = story.StoryId,
                            Type = "videourl",
                            Path = videoUrl
                        };

                        _unitOfWork.storyMedium.Add(mediaurl);
                    }

                    _unitOfWork.Save();

                    var mediaList = new List<StoryMedium>();
                    foreach (var file in Photos)
                    {

                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName).Replace(".", "");
                        var uniqueIdentifier = story.StoryId;
                        var uniqueFileName = $"{fileNameWithoutExtension}_{uniqueIdentifier}.{extension}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", uniqueFileName);
                        if (extension == "png" || extension == "jpg" || extension == "jpeg")
                        {

                            var media = new StoryMedium
                            {
                                StoryId = story.StoryId,
                                Type = "image",
                                Path = uniqueFileName
                            };
                            mediaList.Add(media);
                        }

                        else
                        {


                        }

                        foreach (var media in mediaList)
                        {
                            _unitOfWork.storyMedium.Add(media);
                        }




                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                    }

                    _unitOfWork.Save();



                }

                else
                {
                    var usermediaexist = _unitOfWork.storyMedium.GetFirstOrDefault(s => s.StoryId == userexist.StoryId);
                    var oldMediaPaths = new List<string>();
                    var oldPath = new List<string>();
                    var oldMedia = _unitOfWork.storyMedium.GetAll().Where(m => m.StoryId == userexist.StoryId).ToList();

                    if (oldMedia.Count > 0)
                    {
                        foreach (var media in oldMedia)
                        {
                            var oldMediaPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", media.Path);
                            var oldpath = media.Path;
                            oldPath.Add(oldpath);
                            oldMediaPaths.Add(oldMediaPath);
                        }
                    }
                    // delete the previous images from the server's directory
                    foreach (var old in oldMediaPaths)
                    {
                        if (System.IO.File.Exists(old))
                        {
                            System.IO.File.Delete(old);
                        }


                    }
                    var mediaToDelete = _unitOfWork.storyMedium.GetAll().Where(s => s.StoryId == usermediaexist.StoryId).ToList();


                    foreach (var media in mediaToDelete)
                    {
                        _unitOfWork.storyMedium.Remove(media);
                    }



                    _unitOfWork.Save();


                    userexist.Title = newstory.Title;
                    userexist.PublishedAt = newstory.Date;
                    userexist.Description = newstory.Description;
                    userexist.Status = "DRAFT";
                    userexist.UpdatedAt = DateTime.Now;

                    foreach (var videoUrl in newstory.VideoUrl)
                    {
                        var mediaurl = new StoryMedium
                        {
                            StoryId = userexist.StoryId,
                            Type = "videourl",
                            Path = videoUrl
                        };
                        _unitOfWork.storyMedium.Add(mediaurl);
                    }
                    _unitOfWork.Save();
                    var mediaList = new List<StoryMedium>();

                    foreach (var file in Photos)
                    {

                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName).Replace(".", "");
                        var uniqueIdentifier = userexist.StoryId;
                        var uniqueFileName = $"{fileNameWithoutExtension}_{uniqueIdentifier}.{extension}";
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        var filePath = Path.Combine(uploadPath, uniqueFileName);

                   
                        if (extension == "png" || extension == "jpg" || extension == "jpeg")
                        {

                            var media = new StoryMedium
                            {
                                StoryId = userexist.StoryId,
                                Type = "image",
                                Path = uniqueFileName
                            };
                            mediaList.Add(media);
                        }
                        foreach (var media in mediaList)
                        {
                            _unitOfWork.storyMedium.Add(media);

                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                    }

                    _unitOfWork.Save();

                }

            }
            else
            {
                var misssionId = Convert.ToInt64(newstory.MissionTitle);
                var databaseStoryObj = _unitOfWork.Story.GetFirstOrDefault(s => s.UserId == id && s.MissionId == misssionId);
                databaseStoryObj.Status = "PENDING";
                databaseStoryObj.UpdatedAt = DateTime.Now;
                _unitOfWork.Update(databaseStoryObj);
                _unitOfWork.Save();
            }
            return true;
        }


    }
}
