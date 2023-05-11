using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CommentController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public CommentController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }

    public async Task<IActionResult> LoadCommentPage()
    {
        try
        {
            var comments = await _unitOfService.Comment.GetAllAsync();
            comments = comments.Where(m => m.ApprovalStatus == ApprovalStatus.PENDING);
            return PartialView("_Comment", comments);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading comment: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> AcceptComment(long id)
    {
        try
        {
            await _unitOfService.Comment.UpdateStatusAsync(id, 1);
            var comments = await _unitOfService.Comment.GetAllAsync();
            comments = comments.Where(m => m.ApprovalStatus == ApprovalStatus.PENDING);

            (string, long, long) result = await _unitOfService.Comment.GetDetailsToSendNotification(id);

            SendNotificationVM sendNotificationVM = new SendNotificationVM
            {
                Message = $"Your comment has been approved for this mission - {result.Item1}",
                SettingType = NotificationSettingType.COMMENT,
                NotificationType = NotificationType.APPROVE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{result.Item3}"
            };

            await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
            return PartialView("_Comment", comments);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeclineComment(long id)
    {
        try
        {
            (string, long, long) result = await _unitOfService.Comment.GetDetailsToSendNotification(id);

            await _unitOfService.Comment.DeleteComment(id);
            var comments = await _unitOfService.Comment.GetAllAsync();
            comments = comments.Where(m => m.ApprovalStatus == ApprovalStatus.PENDING);

            SendNotificationVM sendNotificationVM = new()
            {
                Message = $"Your comment has been declined for this mission - {result.Item1}",
                SettingType = NotificationSettingType.COMMENT,
                NotificationType = NotificationType.DECLINE,
                UserId = result.Item2,
                Href = $"/volunteer/mission/missiondetails/{result.Item3}"
            };

            await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
            return PartialView("_Comment", comments);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    public async Task<IActionResult> ViewComment(long id)
    {
        try
        {
            return PartialView("_ViewComment", await _unitOfService.Comment.GetByIdAsync(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading comment: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public async Task<IActionResult> SearchComment(string? query)
    {
        try
        {
            var comments = await _unitOfService.Comment.SearchAsync(query);
            return PartialView("_Comment", comments);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching comment: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
