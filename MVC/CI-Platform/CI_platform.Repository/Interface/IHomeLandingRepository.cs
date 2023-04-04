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
    public interface IHomeLandingRepository
    {
        //HomeLandingPageVM GetLandingPageData(string sort, string email,int currentPage);
        HomeLandingPageVM GetLandingPageData(string email, int currentPage);
        HomeLandingPageVM GetUserProfileData(string email);
    }
}
