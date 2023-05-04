using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
public class NotificationController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public NotificationController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }

    public async Task<IActionResult> GetNotificationPartialByUserId(long userId)
    {
        var notifs = await _unitOfService.UserNotification.GetAllByUserId(userId);
        var setting = await _unitOfService.NotificationSetting.GetNotificationSettingByUserId(userId);
        DateTimeOffset lastCheck = await _unitOfService.UserNotification.GetLastCheckByUserId(userId);

        var newNotifications = new List<UserNotificationVM>();
        var oldNotifications = new List<UserNotificationVM>();
        var unreadCount = 0;

        foreach (var notification in notifs)
        {
            if (notification.LastModified > lastCheck)
            {
                newNotifications.Add(notification);
            }
            else
            {
                oldNotifications.Add(notification);
            }

            if (notification.IsRead == false)
            {
                unreadCount++;
            }
        }

        var userNotifications = new UserNotificationContainerVM
        {
            NotificationSettingVM = setting,
            LastCheck = lastCheck,
            NewNotificationVMs = newNotifications,
            OldNotificationVMs = oldNotifications,
            Unread = unreadCount
        };
        return PartialView("_Notification", userNotifications);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNotificationSetting(NotificationSettingVM setting)
    {
        try
        {
            await _unitOfService.NotificationSetting.UpdateNotificationSetting(setting);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearAllNotification(long userId)
    {
        try
        {
            await _unitOfService.UserNotification.ClearAllNotification(userId);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> MarkAsReadNotification(long notificationId)
    {
        try
        {
            await _unitOfService.UserNotification.MarkAsReadNotification(notificationId);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLastCheck(long userId)
    {
        try
        {
            await _unitOfService.UserNotification.UpdateLastCheck(userId);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
        return NoContent();
    }
}
