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
            if(missions == null)
            {
                ViewBag.MissionError = "true";
                return RedirectToAction("StoryList");
            }
            return View(missions);
        }

        [HttpPost]
        public string ShareStory(string missionId, string title, string description, string videourl, IFormFile files, string action)
        {
            return Url.Action("StoryList");
        }

        public IActionResult StoryList(int? page)
        {
            return View();
        }
    }
}
