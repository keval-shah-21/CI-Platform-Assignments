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
            return PartialView("_Goalsheet", _unitOfService.MissionTimesheet.GetGoalTimesheetAdmin());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult UpdateStatus(long id, byte status, long missionId, int? action, bool isTime)
    {
        try
        {
            _unitOfService.MissionTimesheet.UpdateStatus(id, status);
            if (isTime)
                return PartialView("_Timesheet", _unitOfService.MissionTimesheet.GetHourTimesheetAdmin());
            if (status == 1)
            {
                _unitOfService.MissionGoal.UpdateGoalAchieved(missionId, action);
                _unitOfService.Save();
            }
            return PartialView("_Goalsheet", _unitOfService.MissionTimesheet.GetGoalTimesheetAdmin());
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
            return PartialView("_Goalsheet", _unitOfService.MissionTimesheet.SearchGoalTimehseet(query));
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

    public IActionResult ViewGoal(long id)
    {
        try
        {
            return PartialView("_ViewGoalsheet", _unitOfService.MissionTimesheet.GetTimesheetGoalById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error viewing timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult ViewTime(long id)
    {
        try
        {
            return PartialView("_ViewTimesheet", _unitOfService.MissionTimesheet.GetTimesheetHourById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error viewing timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
