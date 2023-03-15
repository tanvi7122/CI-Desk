using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CI_platform.Controllers
{
    public class Landing_pageController : Controller
    {
        public readonly CiPlatformContext _context;

        public Landing_pageController(CiPlatformContext context)
        {
            _context = context;
        }
        public ActionResult Landing_page()
        {
            //var missions = _context.Missions.ToList();
            var Landing_page = (from e in _context.Missions
                                join t in _context.MissionThemes on e.Theme_id equals t.MissionThemeId
                                join m in _context.MissionRatings on e.MissionId equals m.MissionId
                                select new Landing_page
                                {

                                    MissionId = e.MissionId,
                                    Theme_id = e.Theme_id,
                                    Country_id = e.CountryId,
                                    city_id = e.CityId,
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
                                    Availability = e.Availability,
                                    Rating = m.Rating,
                                    ThemeTitle = t.Title
                                }).ToList();
            return View(Landing_page);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> PlatformLandingPost(string searchText, int? country_id, string? city_id, string? theme_id, string? skillId, int? sortCase, string? userId)
        {
            var response = _context.Missions.FromSql($"exec spFilterSortSearchPagination @searchText={searchText}, @countryId={country_id}, @cityId={city_id}, @themeId={theme_id}, @skillId={skillId}, @sortCase = {sortCase}, @userId = {userId}");

            var items = await response.ToListAsync();

            var MissionIds = items.Select(m => m.MissionId).ToList();

            var list = from m in _context.Missions
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

            return PartialView("_ResultsPartial", list);
            //return Json(list);
        }

        //public ActionResult spFilterSortSearchPagination(string searchText, string sortCase, int theme_id,int skillId,int city_id, int country_id,int userId)
        //{
        //    using (var context = new CiPlatformContext())
        //    {

        //        SqlParameter sortCaseParam = new SqlParameter("@sortCaseParam", SqlDbType.NVarChar) { Value = sortCase };
        //        SqlParameter searchTextParam = new SqlParameter("@searchTextParam", SqlDbType.NVarChar) { Value = searchText }; 
        //        SqlParameter skillIdParam = new SqlParameter("@skillIdParam", SqlDbType.Int) { Value = skillId };
        //        SqlParameter userIdParam = new SqlParameter("@userIdParam", SqlDbType.Int) { Value = userId };
        //        SqlParameter theme_idParam = new SqlParameter("@theme_idParam", SqlDbType.Int) { Value = theme_id };
        //        SqlParameter city_idParam = new SqlParameter("@city_idParam", SqlDbType.Int) { Value = city_id };
        //        SqlParameter country_idParam = new SqlParameter("@country_idParam", SqlDbType.Int) { Value = country_id };

        //        var results = context.Database.SqlQueryRaw<Mission>("spFilterSortSearchPagination @sortCaseParam, @searchTextParam,@theme_id,@city_id,@country_id,@skillIdParam,@userIdParam", skillIdParam,
        //            sortCaseParam,searchTextParam, theme_idParam, city_idParam, country_idParam, userId).ToList();
        //        return PartialView("_Results", results);
        //    }
        //}
        ////public IActionResult FilterByCountryAndCity(int? country_id, int? city_id)
        ////{
        ////    var Results = _context.Missions.FromSqlRaw("EXECUTE spFilterByCountryAndCity @country_id, @city_id",
        ////        new SqlParameter("@country_id", country_id ?? (object)DBNull.Value),
        ////        new SqlParameter("@city_id", city_id ?? (object)DBNull.Value)
        ////    ).ToList();

        ////    return View(Results);
        ////}

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
    







