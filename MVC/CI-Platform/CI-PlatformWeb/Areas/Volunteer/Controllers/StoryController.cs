using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    public class StoryController : Controller
    {
        private readonly IUnitOfService _unitOfService;

        public StoryController(IUnitOfService unitOfService)
        {
            _unitOfService = unitOfService;
        }
        public IActionResult ShareStory()
        {
            long id = string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) ? 0
                : long.Parse(HttpContext.Session.GetString("UserId"));
            if (id == 0) return NotFound();
            List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(id);
            if (missions == null)
            {
                ViewBag.MissionError = "true";
                return RedirectToAction("StoryList");
            }

            StoryVM storyVM = _unitOfService.Story.GetDraftStoryByUserId(id);
            if (storyVM.Title == null)
            {
                ViewBag.draft = "false";
                storyVM = new();
            }else
                ViewBag.draft = "true";
            storyVM.MissionVMs = missions;
            return View(storyVM);
        }

        [HttpPost]
        public IActionResult ShareStory(StoryVM storyVM, IFormFileCollection? ssImagesInput, string action, string isDraft, string? storyId)
        {
            ViewBag.draft = isDraft;
            if (ssImagesInput?.Count() == 0)
            {
                List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(long.Parse(HttpContext.Session.GetString("UserId")));
                storyVM.MissionVMs = missions;
                return View(storyVM);
            }
            if (string.IsNullOrEmpty(storyVM.Description))
            {
                List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(long.Parse(HttpContext.Session.GetString("UserId")));
                storyVM.MissionVMs = missions;
                ModelState.AddModelError("Description", "My Story field is required.");
                return View(storyVM);
            }
            long userId = long.Parse(HttpContext.Session.GetString("UserId")!);
            if(isDraft == "false")
            {
                _unitOfService.Story.SaveStory(storyVM, userId, (byte)(action == "save" ? 3 : 0));
            }
            else
            {
                storyVM.StoryId = long.Parse(storyId);
                _unitOfService.Story.UpdateStory(storyVM, (byte)(action == "save" ? 3 : 0));
            }
            _unitOfService.Save();
            return RedirectToAction("StoryList");
        }

        public IActionResult StoryList(int page)
        {
            List<StoryVM> stories = _unitOfService.Story.GetAll();
            return View(stories.Skip((page - 1) * 9).Take(9).ToList());
        }
    }
}
