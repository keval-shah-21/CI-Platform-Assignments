using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    public class StoryController : Controller
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoryController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfService = unitOfService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult StoryList(string? missionError)
        {
            ViewBag.MissionError = missionError;
            ViewBag.TotalStories = _unitOfService.Story.GetAll().
                Where(s => s.ApprovalStatus == ApprovalStatus.APPROVED).LongCount();
            return View();
        }
        public IActionResult StoryListPartial(int page)
        {
            List<StoryVM> stories = _unitOfService.Story.GetAll().
                Where(s => s.ApprovalStatus == ApprovalStatus.APPROVED)
                .OrderByDescending(s => s.PublishedAt)
                .ToList();
            return PartialView("_StoryList", stories.Skip((page - 1) * 9).Take(9).ToList());
        }
        public IActionResult ShareStory()
        {
            long id = string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) ? 0
                : long.Parse(HttpContext.Session.GetString("UserId"));
            if (id == 0) return NotFound();
            List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(id);
            if (missions.Count == 0)
            {
                return RedirectToAction("StoryList", new {missionError = "true" });
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
        public IActionResult ShareStory(StoryVM storyVM, IFormFileCollection ssImagesInput, string isDraft, string action, List<string>? preLoaded)
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
                List<StoryMediaVM> storyMediaVMs = ssImagesInput.Select((image, i) =>
                    SaveStoryMedia(image, wwwRootPath, latestStoryId)).ToList();
                _unitOfService.StoryMedia.SaveAllStoryMedia(storyMediaVMs);
            }
            else
            {
                preLoaded?.ForEach(image =>
                {
                    if(!ssImagesInput.Any(ss => image == ss.FileName)){
                        System.IO.File.Delete(Path.Combine(wwwRootPath, @"images\story", image));
                        _unitOfService.StoryMedia.RemoveStoryMedia(storyVM.StoryId, image.Split(".")[0]);
                    }
                });
                List<StoryMediaVM> storyMediaVMs = new();
                ssImagesInput.ToList().ForEach(image =>
                {
                    if (!preLoaded.Contains(image.FileName))
                    {
                        storyMediaVMs.Add(SaveStoryMedia(image, wwwRootPath, storyVM.StoryId));
                    }
                });
                if(storyMediaVMs.Count > 0)
                    _unitOfService.StoryMedia.SaveAllStoryMedia(storyMediaVMs);
            }
            _unitOfService.Save();
            return RedirectToAction("StoryList");
        }

        public IActionResult StoryDetails(long? id)
        {
            if(id == null) return NotFound();
            StoryVM story = _unitOfService.Story.GetStoryById(id);
            if (story == null) return NotFound();
            story.TotalViews = story.TotalViews + 1;
            _unitOfService.Story.UpdateTotalViews(story.StoryId, (long)story.TotalViews);
            story.StoryMediaVM = story.StoryMediaVM.ToList();
            return View(story);
        }
        public IActionResult RemoveDraftStory(long storyId)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            StoryVM story = _unitOfService.Story.GetStoryById(storyId);
            story.StoryMediaVM.ForEach(sm =>
            {
                System.IO.File.Delete(Path.Combine(wwwRootPath, @"images\story", sm.MediaName+sm.MediaType));
            });
            _unitOfService.StoryMedia.RemoveAllStoryMediaByStoryId(story.StoryId);
            _unitOfService.Story.RemoveStoryById(story.StoryId);
            _unitOfService.Save();
            return RedirectToAction("StoryList");
        }

        [HttpPost]
        public IActionResult RecommendStory(long storyId, long userId, long[] toUsers)
        {
            var url = Url.Action("StoryDetails", "Story", new { id = storyId }, "https");
            _unitOfService.StoryInvite.RecommendStory(storyId, userId, toUsers, url);
            _unitOfService.Save();
            List<UserVM> users = _unitOfService.User.GetAllUsersToRecommendStory();
            ViewBag.UserId = userId;
            ViewBag.StoryId = storyId;
            return PartialView("_RecommendToCoWorker", users?.Where(u => u.UserId != userId).ToList());
        }

        [NonAction]
        internal StoryMediaVM SaveStoryMedia(IFormFile image, string wwwRootPath, long storyId)
        {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"images\story");
            string extension = Path.GetExtension(image.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }
            return new StoryMediaVM()
            {
                MediaPath = @"\images\story\",
                MediaName = fileName,
                MediaType = extension,
                StoryId = storyId
            };
        }
    }
}
