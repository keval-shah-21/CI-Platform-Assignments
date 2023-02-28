using Microsoft.AspNetCore.Mvc;
using CI_Platform.Entities.ViewModels;
namespace CI_PlatformWeb;

[Area("Volunteer")]
[Route("/volunteer/user/")]
public class UserController: Controller
{
    [Route("login", Name="Login")]
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("login")]
    public IActionResult Login(UserVM user){
        return View();
    }

    [Route("registration", Name="Registration")]
    public IActionResult Registration(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("registration")]
    public IActionResult Registration(UserVM user){
        if(ModelState.IsValid){
            return RedirectToAction("Index", "Home");
        }
        return View(user);
    }

    [Route("forgot-password", Name="ForgotPassword")]
    public IActionResult ForgotPassword(){
        return View();
    }

    [Route("reset-password", Name="ResetPassword")]
    public IActionResult ResetPassword(){
        return View();
    }
}
