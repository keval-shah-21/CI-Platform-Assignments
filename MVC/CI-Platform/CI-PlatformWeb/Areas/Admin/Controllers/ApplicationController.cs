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
            return PartialView("_Application");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading application: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
