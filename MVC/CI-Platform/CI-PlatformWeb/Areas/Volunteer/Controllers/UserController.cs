using Microsoft.AspNetCore.Mvc;
namespace CI_PlatformWeb;

[Area("Volunteer")]
[Route("/volunteer/user/")]
public class UserController: Controller
{
    [Route("login", Name="Login")]
    public IActionResult Login(){
        return View();
    }

    [Route("registration", Name="Registration")]
    public IActionResult Registration(){
        return View();
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
