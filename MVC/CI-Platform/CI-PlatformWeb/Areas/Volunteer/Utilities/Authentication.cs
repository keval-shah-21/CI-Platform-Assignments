using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace CI_PlatformWeb.Areas.Volunteer.Utilities;
public class AuthenticationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Session.GetString("Email") == null)
        {
            var returnUrl = filterContext.HttpContext.Request.Path.Value + QueryHelpers.AddQueryString(filterContext.HttpContext.Request.QueryString.Value, "returnURL", filterContext.HttpContext.Request.Path.Value);
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                { "Controller", "User" },
                { "Action", "Login" },
                {"Area", "Volunteer" },
                { "returnURL", returnUrl }
            });
        }
    }
}