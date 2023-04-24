using CI_platfom.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IAdminRepository
    {
        AdminVM GetUserData(string email);
        AdminVM GetStoryData();
        AdminVM GetMissionData(string email);
        AdminVM GetCmsData(string email);
        AdminVM GetMissionApplicationsData();
        AdminVM ViewStoryData(long id);
        AdminVM GetMissionThemes();
        AdminVM GetMissionSkill();
    }
}
