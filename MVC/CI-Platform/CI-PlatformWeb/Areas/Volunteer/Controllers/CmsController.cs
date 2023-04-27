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
        try
        {
            CmsPageVM cms = _unitOfService.CmsPage.GetCmsPageById(id);
            return View(cms);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        }
    }
    public IActionResult GetCmsList()
    {
        List<CmsPageVM> cmsPageVMs = _unitOfService.CmsPage.GetAll();
        return Json(cmsPageVMs.Select(cms => new { cms.Title, cms.CmsPageId}));
    }
}
