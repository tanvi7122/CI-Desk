    using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Repository
{

        public class UnitOfWork : IUnitOfWork
    {
            private CiPlatformContext _db;
            public IUserRepository User { get; private set; }
            public IPasswordResetRepository PasswordReset { get; private set; }

            public IStoryRepository Story { get; private set; }
          public IStoryMediumRepository storyMedium { get; private set; }
            public ICountryRepository Country { get; private set; }
            public ICityRepository City { get; private set; }

            public IMissionThemeRepository MissionTheme { get; private set; }

            public ISkillRepository Skill { get; private set; }

            public IMissionRepository Mission { get; private set; }

            public IMissionRatingRepository MissionRating { get; private set; }

            public IFavouriteMissionRepository FavoriteMission { get; private set; }

            public IMissionInviteRepository MissionInvite { get; private set; }
            public IMissionSkillRepository MissionSkill { get; private set; }
            public IMissionCommentRepository MissionComment { get; private set; }

            public IMissionDocumentRepository MissionDocument { get; private set; }
            public IMissionApplicationRepository MissionApplication { get; private set; }
            public IMissionMediumRepository MissionMedium { get; private set; }

            public IStoryInviteRepository StoryInvite { get; private set; }
            public IAddStoryRepository AddStory { get; private set; }
            public UnitOfWork(CiPlatformContext db)
            {
                _db = db;
                User = new UserRepository(_db);
                AddStory = new AddStoryRepository(_db);
                Country = new CountryRepository(_db);
                City = new CityRepository(_db);
                MissionTheme = new MissionThemeRepository(_db);
                Skill = new SkillRepository(_db);
                Mission = new MissionRepository(_db);
                Story = new StoryRepository(_db);
               storyMedium = new StoryMediumRepository(_db);
                FavoriteMission = new FavouriteMissionRepository(_db);
                MissionRating = new MissionRatingRepository(_db);
                MissionApplication = new MissionApplicationRepository(_db);
                MissionSkill = new MissionSkillRepository(_db);
                MissionDocument = new MissionDocumentRepository(_db);
                MissionInvite = new MissionInviteRepository(_db);
                MissionMedium = new MissionMediumRepository(_db);
                MissionComment = new MissionCommentRepository(_db);
                StoryInvite = new StoryInviteRepository(_db);
              
            }

            public void Save()
            {
                _db.SaveChanges();
            }
        }
    }


