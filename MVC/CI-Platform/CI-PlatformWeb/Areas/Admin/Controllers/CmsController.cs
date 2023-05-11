using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CmsController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public CmsController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadCMSPage()
    {
        try
        {
            List<CmsPageVM> cms = _unitOfService.CmsPage.GetAll();
            cms = cms.OrderByDescending(c => c.CreatedAt).ToList();
            return PartialView("_CMS_Page", cms);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult AddCMSPage()
    {
        try
        {
            return PartialView("_AddCMS");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading cms addpage: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCMSPage(CmsPageVM cms)
    {
        try
        {
            long id = _unitOfService.CmsPage.SaveCmsPage(cms);
            _unitOfService.Save();

            List<CmsPageVM> cmsList = _unitOfService.CmsPage.GetAll();
            cmsList = cmsList.OrderByDescending(c => c.CreatedAt).ToList();

            SendNotificationVM sendNotificationVM = new()
            {
                Message = $"News - New CMS Page {cms.Title} was recently added by admin.",
                Url = Url.Action("CmsPage", "Cms", new { area = "Volunteer", id }, "https"),
                SettingType = NotificationSettingType.NEWS,
                NotificationType = NotificationType.ADD,
                Href = $"/volunteer/cms/cmspage/{id}"
            };
            await _unitOfService.Notification.SendNotificationToAllUsers(sendNotificationVM, new List<long>());

            return PartialView("_CMS_Page", cmsList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult EditCMSPage(long cmsPageId)
    {
        try
        {
            CmsPageVM cms = _unitOfService.CmsPage.GetCmsPageById(cmsPageId);
            return PartialView("_EditCMS", cms);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading cmsedit: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult EditCMSPage(CmsPageVM cms)
    {
        try
        {
            _unitOfService.CmsPage.UpdateCmsPage(cms);
            _unitOfService.Save();

            List<CmsPageVM> cmsList = _unitOfService.CmsPage.GetAll();
            cmsList = cmsList.OrderByDescending(c => c.CreatedAt).ToList();
            return PartialView("_CMS_Page", cmsList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving cmsedit: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteCMSPage(long cmsPageId)
    {
        try
        {
            _unitOfService.CmsPage.DeleteCmsPage(cmsPageId);

            List<CmsPageVM> cmsList = _unitOfService.CmsPage.GetAll();
            cmsList = cmsList.OrderByDescending(c => c.CreatedAt).ToList();
            return PartialView("_CMS_Page", cmsList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPut]
    public IActionResult DeactivateCMSPage(long cmsPageId)
    {
        try
        {
            _unitOfService.CmsPage.DeactivateCmsPage(cmsPageId);
            _unitOfService.Save();

            List<CmsPageVM> cmsList = _unitOfService.CmsPage.GetAll();
            cmsList = cmsList.OrderByDescending(c => c.CreatedAt).ToList();
            return PartialView("_CMS_Page", cmsList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deactivating cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public bool IsSlugUnique(string slug, long? id)
    {
        if (string.IsNullOrEmpty(slug)) return false;
        return _unitOfService.CmsPage.IsSlugUnique(slug, id);
    }

    public IActionResult SearchCmsPage(string? query)
    {
        try
        {
            List<CmsPageVM> cms = _unitOfService.CmsPage.Search(query);
            cms = cms.OrderByDescending(c => c.CreatedAt).ToList();
            return PartialView("_CMS_Page", cms);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}