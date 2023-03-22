using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    public class StoryController : Controller
    {
        public IActionResult Index()
        {
            return View("ShareStory");
        }
    }
}
