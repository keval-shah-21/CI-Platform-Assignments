using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UserController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfService = unitOfService;
        _webHostEnvironment = webHostEnvironment;
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
        try
        {
            _unitOfService.User.ActivateUserByEmail(email);
            List<UserVM> users = _unitOfService.User.GetAll().ToList();
            return PartialView("_User", users);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error activating user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult AddUser()
    {
        UserAdminVM user = new()
        {
            cityVMs = _unitOfService.City.GetAll(),
            countryVMs = _unitOfService.Country.GetAll()
        };
        return PartialView("_AddUser", user);
    }
    [HttpPost]
    public IActionResult AddUser(UserAdminVM user, IFormFile? profileInput)
    {
        try
        {
            if (_unitOfService.User.GetFirstOrDefaultByEmail(user.Email) == null)
            {
                if (profileInput == null)
                    user.Avatar = @"\images\static\default-profile.webp";
                else
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    user.Avatar = _unitOfService.User.SaveProfileImage(wwwRootPath, profileInput);
                }
                _unitOfService.User.AddUserAdmin(user);
                _unitOfService.Save();
                return StatusCode(200);
            }
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    public void SendAccountMail(string email, string password)
    {
        string token = Guid.NewGuid().ToString();
        var url = Url.Action("Login", "User", new { area = "Volunteer", email = email, token = token }, "https");

        _unitOfService.User.SaveVerifyAccountDetails(email, token);
        _unitOfService.User.SendAccountCreatedMail(email, password, url);
    }
    public IActionResult EditUser(long id)
    {
        try
        {
            UserAdminVM userVM = _unitOfService.User.GetFirstOrDefaultUserAdmin(id);
            userVM.cityVMs = _unitOfService.City.GetAll();
            userVM.countryVMs = _unitOfService.Country.GetAll();
            return PartialView("_EditUser", userVM);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    [HttpPut]
    public IActionResult EditUser(UserAdminVM user, IFormFile? profileInput)
    {
        try
        {
            if (profileInput != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                _unitOfService.User.RemoveProfileImage(wwwRootPath, user.Avatar);
                user.Avatar = _unitOfService.User.SaveProfileImage(wwwRootPath, profileInput);
            }
            _unitOfService.User.UpdateUserAdmin(user);

            _unitOfService.Save();
            return NoContent();
        }
        catch (Exception)
        {
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
