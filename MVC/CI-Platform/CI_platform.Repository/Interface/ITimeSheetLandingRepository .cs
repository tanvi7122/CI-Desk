using CI_platfom.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_platform.Repository.Interface
{
    public interface ITimeSheetLandingRepository
    {
        TimeSheetVM GetTimeSheetPageData(string email);
        TimeSheetVM GetPreviewTimeSheetPageData(string email, long userId, long missionId);
    }
}
