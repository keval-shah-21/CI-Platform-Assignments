using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class StoryController : Controller
{
    public IActionResult LoadStoryPage()
    {
        return PartialView("_Story");
    }
}
