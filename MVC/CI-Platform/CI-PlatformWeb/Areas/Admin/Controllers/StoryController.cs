using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class StoryController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public StoryController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
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
    public IActionResult AcceptStory(long id)
    {
        try
        {
            _unitOfService.Story.AcceptStory(id);
            _unitOfService.Save();
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
    public IActionResult UpdateStatus(long id, byte value)
    {
        try
        {
            _unitOfService.Story.UpdateStatus(id, value);
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
    public IActionResult DeleteStory(long id)
    {
        try
        {
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
