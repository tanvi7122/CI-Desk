using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private readonly CiPlatformContext _context;
        public StoryRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
            public IEnumerable<Story> GetStoryCard()
            {
                var stories = _context.Stories.Include(m => m.StoryMedia).Include(m => m.Mission).Include(m => m.User);
                return stories;
            }
            public Story GetStoryCardById(long id)
            {
                var storyBYId = _context.Stories.Include(m => m.Mission).Include(m => m.User).Include(m => m.StoryInvites).Include(m => m.StoryMedia).FirstOrDefault(m => m.StoryId == id);

                return storyBYId;
            }

            public Story GetPreviewStoryCardById(long userId, long missionId)
            {
                var storyBYId = _context.Stories.Include(m => m.Mission).Include(m => m.User).Include(m => m.StoryInvites).Include(m => m.StoryMedia).FirstOrDefault(u => u.UserId == userId && u.MissionId == missionId && u.Status == "DRAFT");

                return storyBYId;
            }
     
    }

}
