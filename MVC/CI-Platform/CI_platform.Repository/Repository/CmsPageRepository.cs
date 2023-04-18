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
    public class CmsPageRepository : Repository<CmsPage>, ICmsPageRepository
    {
        public CmsPageRepository(CiPlatformContext Context) : base(Context)
        {
        }
    }
}

