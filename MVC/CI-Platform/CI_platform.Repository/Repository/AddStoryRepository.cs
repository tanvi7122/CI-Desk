using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;

namespace CI_platform.Repository.Repository
{
    
        public class AddStoryRepository : IAddStoryRepository
    {
        public readonly CiPlatformContext _CIPlatformContext;

        public AddStoryRepository(CiPlatformContext CIPlatformContext)
        {
            _CIPlatformContext = CIPlatformContext;

        }
        public void Add(StoryLandingPageVM ShareStory)
        {
            Story story = new Story();
            story.Title = ShareStory.Title;
            story.Description = ShareStory.Description;
            story.PublishedAt = ShareStory.PublishedAt;
            story.MissionId = ShareStory.MissionId;
            story.Status = ShareStory.Status;
            StoryMedium storyMedium = new StoryMedium();
            storyMedium.Path = ShareStory.Path;
            storyMedium.Type = ShareStory.Type;
            story.StoryMedia = storyMedium;
            _CIPlatformContext.Add(story);// add the Story instance to the context
            _CIPlatformContext.Add(storyMedium);
        }

        public void save()
        {
            _CIPlatformContext.SaveChanges();
        }



    }
 }

