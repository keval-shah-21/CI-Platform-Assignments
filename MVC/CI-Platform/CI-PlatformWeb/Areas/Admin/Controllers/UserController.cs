using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UserController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public UserController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadUserPage()
    {
        try
        {
            List<UserVM> users = _unitOfService.User.GetAll().ToList();
            return PartialView("_User", users);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult ActivateUser(string email)
    {
        try { 
            _unitOfService.User.ActivateUserByEmail(email);
            List<UserVM> users = _unitOfService.User.GetAll().ToList();
            return PartialView("_User", users);
        }
        catch(Exception e) {
            Console.WriteLine("Error activating user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult DeactivateUser(string email)
    {
        try
        {
            _unitOfService.User.DeactivateUserByEmail(email);
            List<UserVM> users = _unitOfService.User.GetAll().ToList();
            return PartialView("_User", users);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deactivating user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult SearchUser(string? query)
    {
        try
        {
            List<UserVM> users = _unitOfService.User.SearchUser(query).ToList();
            return PartialView("_User", users);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
