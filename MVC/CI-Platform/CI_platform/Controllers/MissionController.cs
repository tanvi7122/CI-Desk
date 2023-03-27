using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CI_platform.Controllers
{
    public class MissionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IHomeLandingRepository _HomeLandingRepository;
        private readonly IMissionLandingRepository _MissionLandingRepository;
        private readonly IConfiguration _config;

        public MissionController(ILogger<HomeController> logger, IConfiguration config, IUnitOfWork unitOfWork, IHomeLandingRepository HomeLandingRepository, IMissionLandingRepository MissionLandingRepository)
        {
            _unitOfWork = unitOfWork;

            this._logger = logger;
            _HomeLandingRepository = HomeLandingRepository;
            _config = config;
            _MissionLandingRepository = MissionLandingRepository;


        }
        public IActionResult GetCitiesByCountry(long countryId)
        {
            var cities = _unitOfWork.City.GetAll().Where(c => c.CountryId == countryId).ToList();
            return Json(cities);
        }

        public IActionResult HomePage(string sort = "", int currentPage = 1)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

            //HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sort, sessionValue,currentPage);
            HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sessionValue, currentPage);
            return View(landingPageData);
        }

      
        [HttpPost]
        public IActionResult AddVolunteer(long missionId, long userId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            var mission_user_comment = _unitOfWork.MissionApplication.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_comment == null)
            {
                _unitOfWork.MissionApplication.Add(new MissionApplication
                {
                    MissionId = missionId,
                    UserId = userId,
                    AppliedAt = DateTime.Now,
                });
            }
            _unitOfWork.Save();

            return Ok();
        }


        public IActionResult MissionDetail(long missionId, long themeid, long cityid, long countryid)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }

            HomeLandingPageVM MissionlandingPageData = _MissionLandingRepository.GetMissionPageData(sessionValue, missionId, themeid, cityid, countryid);
            return View(MissionlandingPageData);
        }

        [HttpPost]
        public IActionResult AddRating(long missionId, long userId, int rating, long themeId, long cityId, long countryId)
        {
            var mission_user_rating = _unitOfWork.MissionRating.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_rating == null)
            {
                _unitOfWork.MissionRating.Add(new MissionRating
                {
                    MissionId = missionId,
                    UserId = userId,
                    Rating = rating,

                });
            }
            else
            {
                _unitOfWork.MissionRating.UpdateRating(mission_user_rating, rating);
            }
            _unitOfWork.Save();
            TempData["success"] = "Rating Successfull";
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("MissionDetail", "Mission", new { missionId, themeId, cityId, countryId });
        }


        [HttpPost]
        public IActionResult AddComment(long missionId, long userId, string comment_text, long themeId, long cityId, long countryId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            var mission_user_comment = _unitOfWork.MissionComment.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_comment == null)
            {
                _unitOfWork.MissionComment.Add(new Comment
                {
                    MissionId = missionId,
                    UserId = userId,
                    CommentText = comment_text,
                });
            }
            _unitOfWork.Save();
            TempData["success"] = "Comment Added!Thank You For Your Comment";

            return RedirectToAction("MissionDetail", "Mission", new { missionId, themeId, cityId, countryId });
        }
        [HttpPost]
        public IActionResult AddToFavorites(long missionId, long userId)
        {
            // Check if the mission is already in favorites for the user
            var mission_user_favourite = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_favourite != null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var FavoriteMissionId = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
                _unitOfWork.FavoriteMission.Remove(FavoriteMissionId);
                _unitOfWork.Save();
                return Ok();


                //return BadRequest("Mission is already in favorites.");
            }

            // Add the mission to favorites for the user
            _unitOfWork.FavoriteMission.Add(new FavouriteMission
            {
                MissionId = missionId,
                UserId = userId,
            });
            TempData["success"] = "Added To Favourite Mission";
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPost]
        public IActionResult AddToFavorite(long missionId, long userId, long themeId, long cityId, long countryId)
        {


            // Check if the mission is already in favorites for the user
            var mission_user_favourite = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_favourite != null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var FavoriteMissionId = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
                _unitOfWork.FavoriteMission.Remove(FavoriteMissionId);
                _unitOfWork.Save();
                return RedirectToAction("MissionDetail", "Mission", new { missionId, themeId, cityId, countryId });


                //return BadRequest("Mission is already in favorites.");
            }

            // Add the mission to favorites for the user
            _unitOfWork.FavoriteMission.Add(new FavouriteMission
            {
                MissionId = missionId,
                UserId = userId,
            });
            TempData["success"] = "Added To Favourite Mission";
            _unitOfWork.Save();
            return RedirectToAction("MissionDetail", "Mission", new { missionId, themeId, cityId, countryId });
        }


        [HttpPost]
        public IActionResult RecommendToCoWorker(long missionId, long fromuserId, long touserId, long theme, long cityid, long countryid)
        {
            // Check if the mission is already in favorites for the user
            var mission_user_recommended = _unitOfWork.MissionInvite.GetFirstOrDefault(u => (u.FromUserId == fromuserId) && (u.MissionId == missionId) && (u.ToUserId == touserId));
            var mission_recommended_theme = _unitOfWork.Missions.GetMissionCardById(missionId);




            if (mission_user_recommended != null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var MissionInviteId = _unitOfWork.MissionInvite.GetFirstOrDefault(u => (u.FromUserId == fromuserId) && (u.MissionId == missionId) && (u.ToUserId == touserId));
                _unitOfWork.MissionInvite.Remove(MissionInviteId);
                _unitOfWork.Save();
                return Ok();


                //return BadRequest("Mission is already in favorites.");
            }
            _unitOfWork.MissionInvite.Add(new MissionInvite
            {
                MissionId = missionId,
                FromUserId = fromuserId,
                ToUserId = touserId
            });
            _unitOfWork.Save();
            var message = new MailMessage();
            var recommendedMissionLink = Url.Action("MissionDetail", "Mission", new { missionId = missionId, themeid = theme, cityid = cityid, countryid = countryid }, Request.Scheme);
            message.Body = recommendedMissionLink;
            var senderEmail = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == fromuserId);
            var receiverEmail = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == touserId);
            message.From = new MailAddress(senderEmail.Email);
            message.To.Add(new MailAddress(receiverEmail.Email));
            message.Subject = $"Recommendation from {senderEmail.FirstName} {senderEmail.LastName} to {receiverEmail.FirstName} {receiverEmail.LastName}";

            // Add the mission to favorites for the user



            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("tanvizankat@gmail.com","xpquelppifdzvdpt");

            // Send the message
            smtpClient.Send(message);

            TempData["success"] = "Coworker Recommended Successfully.";

            return Ok();
        }

        public IActionResult PrivacyPolicy()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

            HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sessionValue, 1);

            return PartialView("_PrivacyPolicy", landingPageData);
        }


    }
}
