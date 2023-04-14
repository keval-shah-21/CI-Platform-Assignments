using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public MissionController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadMissionPage()
    {
        try
        {
            return PartialView("_Mission");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading mission: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
