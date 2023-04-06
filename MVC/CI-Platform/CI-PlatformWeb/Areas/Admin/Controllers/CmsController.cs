using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CmsController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public CmsController( IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadCMSPage()
    {
        List<CmsPageVM> cms = _unitOfService.CmsPage.GetAll();
        return PartialView("_CMS_Page", cms);
    }

    public IActionResult AddCMSPage()
    {
        return PartialView("_AddCMS");
    }

    [HttpPost]
    public IActionResult AddCMSPage(CmsPageVM cms) {
        return RedirectToAction("LoadCMSPage");
    }
}
