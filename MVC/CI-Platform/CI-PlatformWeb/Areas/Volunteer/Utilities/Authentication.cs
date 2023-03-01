using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_PlatformWeb.Areas.Volunteer.Utilities;
public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Session.GetString("Email") == null)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                { "Controller", "Home" },
                { "Action", "Login" }
            });
        }
    }
}