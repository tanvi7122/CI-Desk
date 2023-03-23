
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace CI_platform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IStoryHomeLandingRepository _StoryHomeLandingRepository;
        private readonly IStoryLandingRepository _StoryLandingRepository;
        private readonly IConfiguration _config;

        public StoryController(ILogger<HomeController> logger, IConfiguration config, IUnitOfWork unitOfWork, IStoryHomeLandingRepository StoryHomeLandingRepository, IStoryLandingRepository StoryLandingRepository)
        {
            _unitOfWork = unitOfWork;

            this._logger = logger;
            _StoryHomeLandingRepository = StoryHomeLandingRepository;
            _StoryLandingRepository = StoryLandingRepository;
            _config = config;

        }
        public IActionResult StoryListing()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            StoryLandingPageVM storylandingPageData = _StoryHomeLandingRepository.GetStoryLandingPageData(sessionValue);
            return View(storylandingPageData);

        }

        public IActionResult StoryDetail(long storyId, long missionId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            StoryLandingPageVM storylandingPageData = _StoryLandingRepository.GetStoryPageData(sessionValue, storyId, missionId);
            return View(storylandingPageData);


        }
        [HttpPost]

        public IActionResult ShareYourStory(long storyId, long missionId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            StoryLandingPageVM storylandingPageData = _StoryHomeLandingRepository.GetStoryLandingPageData(sessionValue);
            return View(storylandingPageData);

        }


        [HttpPost]
        public IActionResult RecommendToCoWorker(long storyId, long missionId, long fromuserId, long touserId)
        {
            // Check if the mission is already in favorites for the user
            var story_user_recommended = _unitOfWork.StoryInvite.GetFirstOrDefault(u => (u.FromUserId == fromuserId) && (u.StoryId == storyId) && (u.ToUserId == touserId));
            var story_recommended_theme = _unitOfWork.Story.GetStoryCardById(storyId);




            if (story_user_recommended != null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var StoryInviteId = _unitOfWork.StoryInvite.GetFirstOrDefault(u => (u.FromUserId == fromuserId) && (u.StoryId == storyId) && (u.ToUserId == touserId));
                _unitOfWork.StoryInvite.Remove(StoryInviteId);
                _unitOfWork.Save();
                return Ok();


                //return BadRequest("Mission is already in favorites.");
            }
            _unitOfWork.StoryInvite.Add(new StoryInvite
            {
                StoryId = storyId,
                FromUserId = fromuserId,
                ToUserId = touserId
            });
            _unitOfWork.Save();
            var message = new MailMessage();
            var recommendedMissionLink = Url.Action("StoryDetail", "Story", new { storyId = storyId, missionId = missionId }, Request.Scheme);
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
            smtpClient.Credentials = new NetworkCredential("", "");

            // Send the message
            smtpClient.Send(message);

            TempData["success"] = "Coworker Recommended Successfully.";

            return Ok();
        }




    }
}