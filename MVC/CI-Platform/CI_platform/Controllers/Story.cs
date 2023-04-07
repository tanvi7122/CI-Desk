
using CI_platfom.Entity.Data;
using CI_platfom.Entity.Models;
using CI_platfom.Entity.ViewModel;
using CI_platform.Repository.Interface;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CI_platform.Controllers
{
    public class StoryController : Controller
    {
        public readonly CiPlatformContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IStoryHomeLandingRepository _StoryHomeLandingRepository;
        private readonly IStoryLandingRepository _StoryLandingRepository;
        private readonly IConfiguration _config;
      

        public StoryController(ILogger<HomeController> logger, IConfiguration config, IUnitOfWork unitOfWork, IStoryHomeLandingRepository StoryHomeLandingRepository, IStoryLandingRepository StoryLandingRepository, CiPlatformContext context)
        {
            _unitOfWork = unitOfWork;

            this._logger = logger;
            _StoryHomeLandingRepository = StoryHomeLandingRepository;
            _StoryLandingRepository = StoryLandingRepository;
        
            
            _context = context;
            _config = config;

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
        //public IActionResult ShareYourStory()
        //{
        //    var sessionValue = HttpContext.Session.GetString("UserEmail");
        //    if (String.IsNullOrEmpty(sessionValue))
        //    {
        //        TempData["error"] = "Session Expired!\nPlease Login Again!";

        //        return RedirectToAction("Index", "Home");
        //    }
        //    StoryLandingPageVM GetStorySharepage = _StoryLandingRepository.GetStorySharepage(sessionValue);
        //    return View(GetStorySharepage);

        //}


     
        public JsonResult GetDraftStory(long missionId)
        {
            var userid = HttpContext.Session.GetString("UserId");
            long userId = Convert.ToInt64(userid);
            StoryLandingPageVM storylandingPageData = _StoryLandingRepository.GetPreviewStoryPageData(userId, missionId);
            if (storylandingPageData.AppliedStory != null) 
            {
                List<long> StoryMediaIdList = new List<long>();
                List<string> StoryMediaPathList = new List<string>();
                List<string> StoryMediaVideoUrlList = new List<string>();
                foreach (var item in storylandingPageData.AppliedStory.StoryMedia.Where(s => s.Type != "videourl"))
                {
                    StoryMediaIdList.Add(item.StoryMediaId);
                    StoryMediaPathList.Add(item.Path);
                }
                foreach (var item1 in storylandingPageData.AppliedStory.StoryMedia.Where(s => s.Type != "image"))
                {
                    StoryMediaVideoUrlList.Add(item1.Path);
                }
                JsonResult draftstory = new JsonResult(new
                {
                    title = storylandingPageData.AppliedStory.Title,
                    date = storylandingPageData.AppliedStory.PublishedAt.Value.ToString("yyyy-MM-dd"),
                    description = storylandingPageData.AppliedStory.Description,
                    StoryMediaID = StoryMediaIdList,
                    StoryMediaPath = StoryMediaPathList,
                    StoryMediaVideoUrl = StoryMediaVideoUrlList
                });
                return Json(draftstory);

            }
return new JsonResult("nodraft");
        }
        [HttpPost]
        public JsonResult AddStory(StoryAdd newstory)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";

            }
            var userid = HttpContext.Session.GetString("UserId");
            var Photos = Request.Form.Files.ToList();
            long UserId = Convert.ToInt64(userid);
            var addStory = _StoryHomeLandingRepository.ShareYourStory(UserId, newstory, Photos);

            return Json(new { success = true, message = "Story added successfully with Draft !!" });
        }
        public IActionResult StoryListing(int currentPage = 1)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            StoryLandingPageVM storylandingPageData = _StoryHomeLandingRepository.GetStoryLandingPageData(sessionValue, currentPage);
            return View(storylandingPageData);

        }
        public IActionResult PreviewStory(long missionId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            var tempuserId = HttpContext.Session.GetString("UserId");
            long userId = Convert.ToInt64(tempuserId);
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            StoryLandingPageVM storylandingPageData = _StoryLandingRepository.GetPreviewStoryPageData(userId, missionId);
            return View(storylandingPageData);
        }


        public IActionResult ShareYourStory(long storyId, long missionId, int currentPage = 1)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index", "Home");
            }
            StoryLandingPageVM storylandingPageData = _StoryHomeLandingRepository.GetStoryLandingPageData(sessionValue, currentPage);
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
            smtpClient.Credentials = new NetworkCredential("tanvizankat@gmail.com", "xpquelppifdzvdpt");

            // Send the message
            smtpClient.Send(message);

            TempData["success"] = "Coworker Recommended Successfully.";

            return Ok();
        }




    }
}