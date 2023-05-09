using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_platform.Repository.Interface;
using CI_platfom.Entity.ViewModel;
namespace CI_platform.Repository.Repository
{
    public class LandingPageRepository: ILandingPageRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public LandingPageRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LandingPageVM GetBannerPageData()
        {
            LandingPageVM BannerPageData = new();
            BannerPageData.Banners = _unitOfWork.Banner.GetAll();
            return BannerPageData;

        }
    }
}





