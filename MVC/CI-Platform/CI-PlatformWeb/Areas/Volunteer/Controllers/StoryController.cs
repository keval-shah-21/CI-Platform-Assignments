using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using CI_PlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [AuthenticateAdmin]
    public class StoryController : Controller
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoryController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfService = unitOfService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult StoryList()
        {
            ViewBag.TotalStories = _unitOfService.Story.GetAll().
                Where(s => s.ApprovalStatus == ApprovalStatus.APPROVED).LongCount();
            return View();
        }
        public IActionResult StoryListPartial(int page)
        {
            List<StoryVM> stories = _unitOfService.Story.GetAll().
                Where(s => s.ApprovalStatus == ApprovalStatus.APPROVED)
                .OrderByDescending(s => s.PublishedAt)
                .Skip((page - 1) * 9).Take(9)
                .ToList();
            return PartialView("_StoryList", stories);
        }
        public IActionResult ShareStory()
        {
            long id = string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) ? 0
                : long.Parse(HttpContext.Session.GetString("UserId"));
            if (id == 0)
                return RedirectToAction("Error", "Home", new { area = "Volunteer" });
            List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(id);
            if (missions.Count == 0)
            {
                TempData["MissionError"] = "true";
                return RedirectToAction("StoryList");
            }

            StoryVM storyVM = _unitOfService.Story.GetDraftStoryByUserId(id);
            if (storyVM.Title == null)
            {
                ViewBag.draft = "false";
                storyVM = new();
            }
            else
            {
                ViewBag.draft = "true";
            }
            storyVM.MissionVMs = missions;
            return View(storyVM);
        }

        [HttpPost]
        public IActionResult ShareStory(StoryVM storyVM, List<IFormFile> ssImagesInput, string isDraft, string action, List<string>? preLoaded)
        {
            ViewBag.draft = isDraft;
            long userId = long.Parse(HttpContext.Session.GetString("UserId")!);
            if(isDraft == "false")
            {
                _unitOfService.Story.SaveStory(storyVM, userId, (byte)(action == "save" ? 3 : 0));
            }
            else
            {
                storyVM.StoryId = storyVM.StoryId;
                _unitOfService.Story.UpdateStory(storyVM, (byte)(action == "save" ? 3 : 0));
            }
            _unitOfService.Save();

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(isDraft == "false")
            {
                long latestStoryId = _unitOfService.Story.GetLatestStoryId(userId);
                _unitOfService.StoryMedia.AddAllStoryMedia(wwwRootPath, ssImagesInput, latestStoryId);
            }
            else
            {
                _unitOfService.StoryMedia.EditAllStoryMedia(wwwRootPath, ssImagesInput, storyVM.StoryId, preLoaded);
            }
            _unitOfService.Save();
            return RedirectToAction("StoryList");
        }

        public IActionResult StoryDetails(long? id)
        {
            if(id == null) 
                return RedirectToAction("Error", "Home", new { area = "Volunteer" });
            StoryVM story = _unitOfService.Story.GetStoryById(id);
            if (story == null)
                return RedirectToAction("Error", "Home", new { area = "Volunteer" });
            story.TotalViews = story.TotalViews + 1;
            _unitOfService.Story.UpdateTotalViews(story.StoryId, (long)story.TotalViews);
            story.StoryMediaVM = story.StoryMediaVM.ToList();
            return View(story);
        }
        public IActionResult RemoveDraftStory(long storyId)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            _unitOfService.StoryMedia.RemoveMediaFromFolder(storyId, wwwRootPath);
            _unitOfService.StoryMedia.RemoveAllStoryMediaByStoryId(storyId);
            _unitOfService.Story.RemoveStoryById(storyId);
            _unitOfService.Save();
            return RedirectToAction("StoryList");
        }

        [HttpPost]
        public async Task<IActionResult> RecommendStory(long storyId, long userId, long[] toUsers)
        {
            var url = Url.Action("StoryDetails", "Story", new { id = storyId }, "https");
            await _unitOfService.StoryInvite.RecommendStory(storyId, userId, toUsers, url);
            _unitOfService.Save();
            List<UserVM> users = _unitOfService.User.GetAllUsersToRecommendStory();
            ViewBag.UserId = userId;
            ViewBag.StoryId = storyId;

            string title = await _unitOfService.Story.GetStoryTitleById(storyId);
            UserVM user = await _unitOfService.User.GetFirstOrDefaultById(userId);

            SendNotificationVM sendNotificationVM = new()
            {
                Message = $"{user.FirstName} {user.LastName} - Recommended this Story - {title}",
                SettingType = NotificationSettingType.RECOMMEND_MISSION,
                NotificationType = NotificationType.RECOMMEND,
                FromUserAvatar = user.Avatar,
                Href = $"/volunteer/story/storydetails/{storyId}"
            };
            await _unitOfService.Notification.SendNotificationToAllUsers(sendNotificationVM, toUsers.ToList());

            return PartialView("_RecommendToCoWorker", users?.Where(u => u.UserId != userId).ToList());
        }
    }
}
