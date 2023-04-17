using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ApplicationController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public ApplicationController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadApplicationPage()
    {
        try
        {
            List<MissionApplicationVM> applications = _unitOfService.MissionApplication.GetAllWithInclude();
            applications = applications.OrderBy(m => m.ApprovalStatus == 0).ToList();
            return PartialView("_Application", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading application: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult UpdateStatus(long id, byte value)
    {
        try
        {
            _unitOfService.MissionApplication.UpdateStatus(id, value);  
            List<MissionApplicationVM> applications = _unitOfService.MissionApplication.GetAllWithInclude();
            applications = applications.OrderBy(m => m.ApprovalStatus == 0).ToList();
            return PartialView("_Application", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating status: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult ViewApplication(long id)
    {
        try
        {
            return PartialView("_ViewApplication", _unitOfService.MissionApplication.GetById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading application: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult SearchApplication(string? query)
    {
        try
        {
            List<MissionApplicationVM> applications = _unitOfService.MissionApplication.Search(query);
            applications = applications.OrderBy(m => m.ApprovalStatus == 0).ToList();
            return PartialView("_Application", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching application: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
