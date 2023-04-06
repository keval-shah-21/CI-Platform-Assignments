using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionController : Controller
{
    public IActionResult LoadMissionPage()
    {
        return PartialView("_Mission");
    }
    public IActionResult LoadSkillPage()
    {
        return PartialView("_Skill");
    }
    public IActionResult LoadThemePage()
    {
        return PartialView("_Theme");
    }
    public IActionResult LoadApplicationPage()
    {
        return PartialView("_Application");
    }
    public IActionResult LoadTimesheetPage()
    {
        return PartialView("_Timesheet");
    }
}
