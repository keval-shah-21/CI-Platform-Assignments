using CI_Platform.Entities.Constants;
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
            List<MissionApplicationVM> applications = _unitOfService.MissionApplication.GetAllAdmin();
            applications = applications.OrderByDescending(m => m.ApprovalStatus == 0).ToList();
            return PartialView("_Application", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading application: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatusAsync(long id, byte value)
    {
        try
        {
            _unitOfService.MissionApplication.UpdateStatus(id, value);
            List<MissionApplicationVM> applications = _unitOfService.MissionApplication.GetAllAdmin();
            applications = applications.OrderByDescending(m => m.ApprovalStatus == 0).ToList();

            (string, long) result = await _unitOfService.MissionApplication.GetMissionNameToSendNotification(id);
            SendNotificationVM sendNotificationVM = new(); 
            if (value == 1)
            {
                sendNotificationVM = new SendNotificationVM
                {
                    Message = $"Volunteering request has been approved for this mission - {result.Item1}",
                    SettingType = NotificationSettingType.MISSION_APPLICATION,
                    NotificationType = NotificationType.APPROVE,
                    UserId = result.Item2
                };
            }
            else
            {
                sendNotificationVM = new SendNotificationVM
                {
                    Message = $"Volunteering request has been declined for this mission - {result.Item1}",
                    SettingType = NotificationSettingType.MISSION_APPLICATION,
                    NotificationType = NotificationType.DECLINE,
                    UserId = result.Item2
                };
            }
            await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
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
            applications = applications.OrderByDescending(m => m.ApprovalStatus == 0).ToList();
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
