using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
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
            List<MissionTimesheetVM> mts = _unitOfService.MissionTimesheet.GetHourTimesheetAdmin();
            return PartialView("_Timesheet", mts.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
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
            List<MissionTimesheetVM> mts = _unitOfService.MissionTimesheet.GetGoalTimesheetAdmin();
            return PartialView("_Goalsheet", mts.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public async Task<IActionResult> UpdateStatus(long id, byte status, long missionId, int? action, bool isTime)
    {
        try
        {
            _unitOfService.MissionTimesheet.UpdateStatus(id, status);

            if (isTime)
            {
                await SendHourNotification(id, status, missionId);
                List<MissionTimesheetVM> mts = _unitOfService.MissionTimesheet.GetHourTimesheetAdmin();
                return PartialView("_Timesheet", mts.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
            }
            await SendGoalNotification(id, status, missionId);
            if (status == 1)
            {
                bool isAchieved = _unitOfService.MissionGoal.UpdateGoalAchieved(missionId, action);
                if(isAchieved) _unitOfService.Mission.CloseMission(missionId);
                _unitOfService.Save();
            }
            List<MissionTimesheetVM> mgs = _unitOfService.MissionTimesheet.GetGoalTimesheetAdmin();
            return PartialView("_Goalsheet", mgs.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating status: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    private async Task SendHourNotification(long id, byte status,long missionId)
    {
        (string, long) result = await _unitOfService.MissionTimesheet.GetDetailsToSendNotification(id);
        SendNotificationVM sendNotificationVM;
        if (status == 1)
        {
            sendNotificationVM = new SendNotificationVM
            {
                Message = $"our volunteering hour request has been approved for this mission - {result.Item1}",
                SettingType = NotificationSettingType.VOLUNTEERING_HOUR,
                NotificationType = NotificationType.APPROVE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{missionId}"
            };
        }
        else
        {
            sendNotificationVM = new SendNotificationVM
            {
                Message = $"Your volunteering hour request has been declined for this mission - {result.Item1}",
                SettingType = NotificationSettingType.VOLUNTEERING_HOUR,
                NotificationType = NotificationType.DECLINE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{missionId}"
            };
        }
        await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
    }
    private async Task SendGoalNotification(long id, byte status, long missionId)
    {
        (string, long) result = await _unitOfService.MissionTimesheet.GetDetailsToSendNotification(id);
        SendNotificationVM sendNotificationVM;
        if (status == 1)
        {
            sendNotificationVM = new SendNotificationVM
            {
                Message = $"Your volunteering goal request has been approved for this mission - {result.Item1}",
                SettingType = NotificationSettingType.VOLUNTEERING_GOAL,
                NotificationType = NotificationType.APPROVE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{missionId}"
            };
        }
        else
        {
            sendNotificationVM = new SendNotificationVM
            {
                Message = $"Your volunteering goal request has been declined for this mission - {result.Item1}",
                SettingType = NotificationSettingType.VOLUNTEERING_GOAL,
                NotificationType = NotificationType.DECLINE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{missionId}"
            };
        }
        await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
    }

    public IActionResult SearchGoalsheet(string? query)
    {
        try
        {
            List<MissionTimesheetVM> mts = _unitOfService.MissionTimesheet.SearchGoalTimehseet(query);
            return PartialView("_Goalsheet", mts.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
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
            List<MissionTimesheetVM> mts = _unitOfService.MissionTimesheet.SearchHourTimehseet(query);
            return PartialView("_Timesheet", mts.OrderByDescending(m => m.ApprovalStatus == ApprovalStatus.PENDING).ToList());
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
