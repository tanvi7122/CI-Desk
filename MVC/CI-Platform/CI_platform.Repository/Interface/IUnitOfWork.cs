using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IUnitOfWork
    {
        public IUserRepository User{ get; }
        public IPasswordResetRepository PasswordReset{ get; }
       
        public ICountryRepository Country{ get; }

        public ICityRepository City{ get; }

        public IMissionThemeRepository MissionTheme{ get; }

        public ISkillRepository Skill{ get; }
        public IStoryMediumRepository storyMedium { get; }
        public IMissionRepository Mission{ get; }
         
        public IStoryRepository Story{ get; }

        public IMissionRatingRepository MissionRating { get; }

        public IMissionInviteRepository MissionInvite { get; }
       public IFavouriteMissionRepository FavoriteMission{ get; }

        public IMissionSkillRepository MissionSkill{ get; }

        public IMissionMediumRepository MissionMedium{ get; }
        public IMissionCommentRepository MissionComment{ get; }

        public IMissionDocumentRepository MissionDocument{ get; }
        public IMissionApplicationRepository MissionApplication { get; }

        public IStoryInviteRepository StoryInvite { get; }
        void Update(CI_platfom.Entity.Models.Story databaseStoryObj);
        void Save();
    }
}
