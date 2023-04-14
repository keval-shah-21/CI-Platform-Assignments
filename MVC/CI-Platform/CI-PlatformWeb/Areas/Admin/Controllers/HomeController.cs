using CI_Platform.Services.Service.Interface;
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
        return View();
    }

    //public IActionResult AdminProfile(long id)
    //{
    //    if (id == 0) return NotFound();
    //    ProfileVM user = _unitOfService.User.GetUserProfileById(id);
    //    if (user == null) return NotFound();
    //    return View(user);
    //}
}
