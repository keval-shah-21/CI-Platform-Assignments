using Microsoft.AspNetCore.Mvc;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/volunteer/user/")]
public class UserController: Controller
{
    private readonly IUnitOfService _unitOfService;

    public UserController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }

    [Route("login", Name="Login")]
    public IActionResult Login(){
        return View();
    }

    [Route("logout", Name="Logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("login", Name = "LoginPost")]
    public IActionResult Login(LoginVM loginVM){
        if(ModelState.IsValid){
            UserVM userVM = _unitOfService.User.Login(loginVM);
            if(userVM != null){
                SetUserLoginSession(userVM);
                return RedirectToAction("Index", "Home");
            }
        }
        return View(loginVM);
    }

    public void SetUserLoginSession(UserVM userVM){
        HttpContext.Session.SetString("FirstName", userVM.FirstName.ToString());
        HttpContext.Session.SetString("LastName", userVM.LastName.ToString());
        HttpContext.Session.SetString("UserId", userVM.UserId.ToString());
        HttpContext.Session.SetString("Email", userVM.Email.ToString());
    }

    public void SetUserProfileSession(){

    }

    [Route("registration", Name="Registration")]
    public IActionResult Registration(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("registration", Name="RegistrationPost")]
    public IActionResult Registration(UserVM userVM){
        if(ModelState.IsValid){
            _unitOfService.User.Add(userVM);
            _unitOfService.Save();
            SetUserLoginSession(userVM);
            return RedirectToAction("Index", "Home");
        }
        return View(userVM);
    }

    [Route("forgot-password", Name="ForgotPassword")]
    public IActionResult ForgotPassword(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("forgot-password", Name="ForgotPasswordPost")]
    public IActionResult ForgotPassword(string? Email){
        if(Email == null){
            return NotFound();
        }
        return View();
    }

    [Route("reset-password", Name="ResetPassword")]
    public IActionResult ResetPassword(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("reset-password", Name="ResetPasswordPost")]
    public IActionResult ResetPassword(string? Password){
        return View();
    }
}
