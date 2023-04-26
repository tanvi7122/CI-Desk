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
        AdminVM GetStoryData(string email);
        AdminVM GetMissionData(string email);
        AdminVM GetCmsData(string email);
        AdminVM GetMissionApplicationsData(string email);
        AdminVM ViewStoryData(long id, string email);
        AdminVM GetMissionThemes(string email);
        AdminVM GetMissionSkill(string email);
        AdminVM GetBanner(string email);
        AdminVM GetMissionPageData(string email);
    }
}
