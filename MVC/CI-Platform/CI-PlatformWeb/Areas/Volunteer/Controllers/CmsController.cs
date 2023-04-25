using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using CI_PlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
[AuthenticateAdmin]
public class CmsController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public CmsController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult CmsPage(long id)
    {
        if(id == 0)
        {
            RedirectToAction("Index", "Home");
        }
        CmsPageVM cms = _unitOfService.CmsPage.GetCmsPageById(id);
        return View(cms);
    }
    public IActionResult GetCmsList()
    {
        List<CmsPageVM> cmsPageVMs = _unitOfService.CmsPage.GetAll();
        return Json(cmsPageVMs);
    }
}
