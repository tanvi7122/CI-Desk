using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface IMissionLandingRepository
    {
        HomeLandingPageVM GetMissionPageData(string email,long missionId,long themeid,long cityid,long countryid);
    }
}
