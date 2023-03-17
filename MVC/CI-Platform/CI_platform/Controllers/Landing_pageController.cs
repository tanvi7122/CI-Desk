using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CI_platform.Controllers
{
    public class Landing_pageController : Controller
    {
        public readonly CiPlatformContext _context;
        private readonly IMissionList _missionlist;
        private readonly IMissionDetail _missiondetail;

        public Landing_pageController(CiPlatformContext context, IMissionList missionlist, IMissionDetail missiondetail)
        {
            _context = context;
            _missionlist = missionlist;
            _missiondetail = missiondetail;

        }


        //GET
 

        public IActionResult GetCitiesByCountry(int countryId)
        {
            var cities = _context.Cities.Where(c => c.CountryId == countryId).ToList();
            return Json(cities);
        }


        [HttpPost]
        public IActionResult AddToFavorites(int missionId)
        {
            string Id = HttpContext.Session.GetString("UserId");
            long userId = long.Parse(Id);

            // Check if the mission is already in favorites for the user
            if (_context.FavouriteMissions.Any(fm => fm.MissionId == missionId && fm.UserId == userId))
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var FavoriteMissionId = _context.FavouriteMissions.Where(fm => fm.MissionId == missionId && fm.UserId == userId).FirstOrDefault();
                _context.FavouriteMissions.Remove(FavoriteMissionId);
                _context.SaveChanges();
                return RedirectToAction("_FilterPartial", "Landing_page");

                //return BadRequest("Mission is already in favorites.");
            }

            // Add the mission to favorites for the user
            var favoriteMission = new FavouriteMission { MissionId = missionId, UserId = userId };
            _context.FavouriteMissions.Add(favoriteMission);
            _context.SaveChanges();

            return RedirectToAction("_FilterPartial", "Landing_page");
        }


        public ActionResult Landing_page()
        {
            //var missions = _context.Missions.ToList();
            var Landing_page = (from m in _context.Mission
                                join t in _context.MissionThemes on m.Theme_id equals t.MissionThemeId
                                join cn in _context.Countries on m.CountryId equals cn.CountryId
                                join ct in _context.Cities on m.CityId equals ct.CityId
                                join r in _context.MissionRatings on m.MissionId equals r.MissionId
                                join goal in _context.GoalMissions on m.MissionId equals goal.MissionId into x
                                from g in x.DefaultIfEmpty()
                                join img in _context.MissionMedia on m.MissionId equals img.MissionId into y
                                from i in y.DefaultIfEmpty()
                                select new Landing_page
                                {

                                    MissionId = m.MissionId,
                                    Theme_id = m.Theme_id,
                                    Country_id = m.CountryId,
                                    city_id = m.CityId,
                                    Title = m.Title,
                                    ShortDescription = m.ShortDescription,
                                    Description = m.Description,
                                    OrganizationName = m.OrganizationName,
                                    StartDate = m.StartDate,
                                    EndDate = m.EndDate,
                                    MissionType = m.MissionType,
                                    CreatedAt = m.CreatedAt,
                                    UpdatedAt = m.UpdatedAt,
                                    DeletedAt = m.DeletedAt,
                                    CityName = ct.CityName,
                                    Country=cn.Name,
                                    Availability = m.Availability,
                                    Rating = r.Rating,
                                    ThemeTitle = t.Title
                                }).ToList();
   

            return View(Landing_page);
        }


        //GET
        public IActionResult _FilterPartial()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.UserId = HttpContext.Session.GetString("UserId");
            }

            var country = _context.Countries.ToList();
            var countryall = new SelectList(country, "CountryId", "Name");
            ViewBag.CountryList = countryall;

            var skill = _context.Skills.ToList();
            var skillall = new SelectList(skill, "SkillId", "SkillName");
            ViewBag.SkillList = skillall;

            var theme = _context.MissionThemes.ToList();
            var themeall = new SelectList(theme, "MissionThemeId", "Title");
            ViewBag.ThemeList = themeall;

            return PartialView("_FilterPartial.cshtml");
        }
       
        [HttpPost]
        public async Task<IActionResult> PlatformLandingPost(string searchText, int? country_id, string? city_id, string? theme_id, string? skillId, int? sortCase, string? userId)
        {
            var response = _context.Mission.FromSql($"exec spFilterSortSearchPagination @searchText={searchText}, @countryId={country_id}, @cityId={city_id}, @themeId={theme_id}, @skillId={skillId}, @sortCase = {sortCase}, @userId = {userId}");

            var items = await response.ToListAsync();

            var MissionIds = items.Select(m => m.MissionId).ToList();

            var Landing_page = from m in _context.Mission
                               join cn in _context.Countries on m.CountryId equals cn.CountryId
                               join ct in _context.Cities on m.CityId equals ct.CityId
                               join t in _context.MissionThemes on m.Theme_id equals t.MissionThemeId
                               join goal in _context.GoalMissions on m.MissionId equals goal.MissionId into x
                               from g in x.DefaultIfEmpty()
                               join img in _context.MissionMedia on m.MissionId equals img.MissionId into y
                               from i in y.DefaultIfEmpty()
                               where MissionIds.Contains(m.MissionId)

                               select new Landing_page
                               {
                                   MissionId = m.MissionId,
                                   Title = m.Title,
                                   ShortDescription = m.ShortDescription,
                                   MissionType = m.MissionType,
                                   OrganizationName = m.OrganizationName,
                                   StartDate = m.StartDate,
                                   EndDate = m.EndDate,
                                   ThemeTitle = t.Title,
                                   CityName = ct.CityName,
                                   Country_id = cn.CountryId,
                                   GoalObjectiveText = g.GoalObjectiveText,
                                   MediaPath = i.MediaPath
                               };

            return PartialView("_ResultsPartial.cshtml", Landing_page);
            //return Json(list);
        }


    }
}


//    var searchResult = new List<Mission>();
//using (var conn = new SqlConnection(ConnectionStrings))
//{
//    conn.Open();
//    using (var cmd = new SqlCommand("spFilterSortSearchPagination", conn))
//    {
//        cmd.CommandType = CommandType.StoredProcedure;

//        // Add any required parameters to the command object
//        cmd.Parameters.AddWithValue("@short_description", short_description);
//        cmd.Parameters.AddWithValue("@title", title);
//        cmd.Parameters.AddWithValue("@organization_name", organization_name);

//        //...

//        using (var reader = cmd.ExecuteReader())
//        {
//            // Read the result set and populate the searchResult variable
//            while (reader.Read())
//            {
//                var result = new Mission
//                {
//                    // Map the column values to the properties of the search result model
//                    Property1 = reader.GetString(0),
//                    Property2 = reader.GetInt32(1),
//                    // ...
//                };
//                searchResult.Add(result);
//            }
//        }


//        var results = _context.Database.SqlQueryRaw<Mission>("exec SearchProc @searchQuery", new SqlParameter("@searchQuery", searchQuery)).ToList();
//        return Json(results);


//    //var mission = _context.Missions.FromSqlRaw($"EXEC SearchMission @searchQuery={searchQuery}").ToList();

//    //return Ok(mission);








