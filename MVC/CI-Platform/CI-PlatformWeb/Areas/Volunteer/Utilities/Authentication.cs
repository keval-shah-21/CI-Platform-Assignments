using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_PlatformWeb.Areas.Volunteer.Utilities;
public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var arguments = filterContext.ActionArguments;
        var userId = arguments["userId"];
        if (filterContext.HttpContext.Session.GetString("Email") == null)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                { "Controller", "User" },
                { "Action", "Login" }
            });
        } else if (filterContext.HttpContext.Session.GetString("UserId") != userId.ToString())
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                { "Controller", "Home" },
                { "Action", "Index" }
            });
        }

    }
}