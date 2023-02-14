using Microsoft.AspNetCore.Mvc;
namespace CI_PlatformWeb;

public class UserController: Controller
{
    public IActionResult Login(){
        return View();
    }
    public IActionResult Registration(){
        return View();
    }
    public IActionResult ForgotPassword(){
        return View();
    }
    public IActionResult ResetPassword(){
        return View();
    }
}
