using CI_Platform.Entities.Constants;
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
    public async Task<IActionResult> UpdateStatus(long id, byte value)
    {
        try
        {
            await _unitOfService.Comment.UpdateStatusAsync(id, value);
            var comments = await _unitOfService.Comment.GetAllAsync();
            comments = comments.Where(m => m.ApprovalStatus == ApprovalStatus.PENDING);
            return PartialView("_Comment", comments);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating status: " + e.Message);
            Console.WriteLine(e.StackTrace);
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

    public async Task<IActionResult> Searchcomment(string? query)
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
