using CI_Platform.Services.Service.Interface;
using CI_PlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public HomeController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }

    //[Authentication]
    public IActionResult Index()
    {
        //if (_unitOfService.User.GetFirstOrDefaultAdminByEmail(HttpContext.Session.GetString("Email")) == null)
        //{
        //    return RedirectToAction("Index", "Home", new { area = "Volunteer" });
        //}
        return View();
    }
}