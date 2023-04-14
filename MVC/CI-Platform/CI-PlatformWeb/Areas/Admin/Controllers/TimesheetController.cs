using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class TimesheetController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public TimesheetController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadTimesheetPage()
    {
        try
        {
            return PartialView("_Timesheet", _unitOfService.MissionTimesheet.GetHourTimesheetAdmin());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult LoadGoalsheetPage()
    {
        try
        {
            return PartialView("_Timesheet", _unitOfService.MissionTimesheet.GetHourTimesheetAdmin());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult UpdateStatus(long id, byte value)
    {
        try
        {
            _unitOfService.MissionTimesheet.UpdateStatus(id, value);
            return PartialView("_Timesheet", _unitOfService.MissionTimesheet.GetHourTimesheetAdmin());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating status: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult SearchGoalsheet(string? query)
    {
        try
        {
            return PartialView("_Timesheet", _unitOfService.MissionTimesheet.SearchGoalTimehseet(query));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult SearchTimesheet(string? query)
    {
        try
        {
            return PartialView("_Timesheet", _unitOfService.MissionTimesheet.SearchHourTimehseet(query));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
