using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class StoryController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public StoryController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfService = unitOfService;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult LoadStoryPage()
    {
        try
        {
            return PartialView("_Story", _unitOfService.Story.GetAdminStories());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult ViewStory(long id)
    {
        try
        {
            return View("ViewStory", _unitOfService.Story.GetStoryById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error viewing story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> AcceptStory(long id)
    {
        try
        {
            _unitOfService.Story.AcceptStory(id);
            _unitOfService.Save();
            await SendApproveNotification(id);
            return PartialView("_Story", _unitOfService.Story.GetAdminStories());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error accepting story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatus(long id, byte value)
    {
        try
        {
            _unitOfService.Story.UpdateStatus(id, value);
            if (value == 2)
                await SendDeclineNotification(id);
            else
                await SendApproveNotification(id);
            
            return PartialView("_Story", _unitOfService.Story.GetAdminStories());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStory(long id)
    {
        try
        {
            StoryVM story = await _unitOfService.Story.GetFirstOrDefaultAsync(id);
            if (story.ApprovalStatus == ApprovalStatus.PENDING)
                await SendDeclineNotification(id);

            _unitOfService.Story.GetStoryById(id);
            _unitOfService.StoryInvite.RemoveByStoryId(id);
            _unitOfService.StoryMedia.RemoveMediaFromFolder(id, _webHostEnvironment.WebRootPath);
            _unitOfService.StoryMedia.RemoveAllStoryMediaByStoryId(id);
            _unitOfService.Save();
            _unitOfService.Story.DeleteStory(id);
            return PartialView("_Story", _unitOfService.Story.GetAdminStories());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    private async Task SendApproveNotification(long id)
    {
        (string, long) result = await _unitOfService.Story.GetDetailsToSendNotification(id);
        SendNotificationVM sendNotificationVM = new()
        {
            Message = $"Your story {result.Item1} has been approved.",
            SettingType = NotificationSettingType.MY_STORY,
            NotificationType = NotificationType.APPROVE,
            UserId = result.Item2,
            Href = $"/volunteer/story/storydetails/{id}"
        };
        await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
    }

    private async Task SendDeclineNotification(long id)
    {
        (string, long) result = await _unitOfService.Story.GetDetailsToSendNotification(id);
        SendNotificationVM sendNotificationVM = new()
        {
            Message = $"Your story {result.Item1} has been declined.",
            SettingType = NotificationSettingType.MY_STORY,
            NotificationType = NotificationType.DECLINE,
            UserId = result.Item2
        };
        await _unitOfService.Notification.SendUserNotification(sendNotificationVM);
    }

    public IActionResult SearchStory(string? query)
    {
        try
        {
            return PartialView("_Story", _unitOfService.Story.Search(query));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
