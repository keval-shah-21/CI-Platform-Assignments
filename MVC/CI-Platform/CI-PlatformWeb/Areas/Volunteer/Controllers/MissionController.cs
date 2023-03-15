using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class MissionController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MissionDetails()
    {
        //if (id == 0) return NotFound();
        return View();
    }
}
