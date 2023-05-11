using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Linq;
using System.Web;
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
        [Authorize(Roles = "user")]
        public IActionResult HomePage(string sort = "", int currentPage = 1)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Login","Home");
            }
            var pageSize = 3;
            int page = currentPage;
            HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sessionValue, currentPage);
            landingPageData.Mission = _unitOfWork.Mission.GetAll()
          .Where(m => m.Status == 1 && m.DeletedAt == null)
            .OrderByDescending(m => m.StartDate)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToList();

            // Set the total number of missions to be used for pagination
            landingPageData.TotalMission = _unitOfWork.Mission.GetAll()
                .Where(m => m.Status == 1 && m.DeletedAt == null)
                .Count();

            // Calculate the total number of pages needed for pagination
            landingPageData.TotalPages = (int)Math.Ceiling((double)landingPageData.TotalMission / pageSize);

            // Set the current page number
            landingPageData.CurrentPage = page;

           
            return View(landingPageData);
        }

      
        [HttpPost]
        public IActionResult AddVolunteer(long missionId, long userId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Login", "Home");
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
            TempData["success"] = ToastrMessages.AccountUpdatedMessage;
            _unitOfWork.Save();

            return Ok();
        }


        [Authorize(Roles = "user")]
        public IActionResult MissionDetail(long missionId, long themeid, long cityid, long countryid)
         {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Login", "Home");
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
            return Json(new { success = true });
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
            if (comment_text == null)
            {
                TempData["error"] = "Please Enter Comment";
            }
            else
            {
                _unitOfWork.Save();
                TempData["success"] = "Comment Added!Thank You For Your Comment";
            }
           

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
        public IActionResult AddToFavorite(long missionId, long userId)
        {


            // Check if the mission is already in favorites for the user
            var mission_user_favourite = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_favourite != null)
            {
          
                var FavoriteMissionId = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
                _unitOfWork.FavoriteMission.Remove(FavoriteMissionId);
                _unitOfWork.Save();
                TempData["success"] = ToastrMessages.AccountUpdatedMessage;

            }
            else
            {
                // Add the mission to favorites for the user
                _unitOfWork.FavoriteMission.Add(new FavouriteMission
                {
                    MissionId = missionId,
                    UserId = userId,
                });
                _unitOfWork.Save();
                TempData["success"] = ToastrMessages.CommentAddedMessage;
            }
        
            return RedirectToAction("MissionDetail", "Mission", new { missionId });
        }


        [HttpPost]
        public IActionResult RecommendToCoWorker(long missionId, long fromuserId, long touserId, long themeId, long cityId, long countryId)
        {
            // Check if the mission is already in favorites for the user
            var mission_user_recommended = _unitOfWork.MissionInvite.GetFirstOrDefault(u => (u.FromUserId == fromuserId) && (u.MissionId == missionId) && (u.ToUserId == touserId));
            var mission_recommended_theme = _unitOfWork.Mission.GetMissionCardById(missionId);




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
            var recommendedMissionLink = Url.Action("MissionDetail", "Mission", new { missionId = missionId, themeid = themeId, cityid = cityId, countryid = countryId }, Request.Scheme);
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
          

            HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sessionValue, 1);

            return View("PrivacyPolicy", landingPageData);
        }

        [HttpPost]
        public IActionResult ContactUs(ContactU model)
        {
            var userid = HttpContext.Session.GetString("UserId");
            long userId = Convert.ToInt64(userid);
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            // Save the data to the database
            _unitOfWork.ContactU.Add(new ContactU
            {

                UserId = userId,
                Subject = model.Subject,
                Message = model.Message,
                CreatedAt = DateTime.Now,

            });
            _unitOfWork.Save();
            return View(HomePage);
        }


    }
}
