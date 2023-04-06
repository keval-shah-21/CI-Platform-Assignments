using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UserController : Controller
{
    public IActionResult LoadUserPage()
    {
        return PartialView("_User");
    }
    public IActionResult LoadContactPage()
    {
        return PartialView("_Contact");
    }
}
