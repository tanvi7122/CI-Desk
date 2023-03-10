using CI_platfom.Entity.Data;
using CI_platfom.Entity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class Landing_pageController : Controller
    {
        public readonly CiPlatformContext _context;

        public Landing_pageController( CiPlatformContext context)
        {
            _context = context;
        }
        public ActionResult Landing_page()
        {
            //var missions = _context.Missions.ToList();
            var Landing_page =( from e in _context.Missions
                                join t in _context.MissionThemes on e.ThemeId equals t.MissionThemeId 
                               join m in _context.MissionRatings on e.MissionId equals m.MissionId  select new Landing_page
                               {

                                   MissionId = e.MissionId,
                ThemeId = e.ThemeId,
                CountryId = e.CountryId,
                CityId = e.CityId,
                Title = e.Title,
                ShortDescription = e.ShortDescription,
                Description = e.Description,
                OrganizationName = e.OrganizationName,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                MissionType = e.MissionType,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                DeletedAt = e.DeletedAt,
               Availability=e.Availability,
               Rating = m.Rating,
             ThemeTitle = t.Title
            }).ToList();
            return View(Landing_page);

        }


     }
}


