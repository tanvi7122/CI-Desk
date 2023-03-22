using CI_platform.Repository.Interface;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CI_platform.Repository.Repository
{
    public class MissionRatingRepository:Repository<MissionRating>, IMissionRatingRepository
    {
        private readonly CiPlatformContext _context;

        public MissionRatingRepository(CiPlatformContext context) : base(context)
        {
            _context = context;
        }
        public void Update(MissionRating missionRating)
        {
            throw new NotImplementedException();
        }
        public void UpdateRating(MissionRating missionRating, int rating)
        {
            missionRating.Rating = rating;
            missionRating.UpdatedAt = DateTime.Now;
            _context.MissionRatings.Update(missionRating);
        }
    }
}
