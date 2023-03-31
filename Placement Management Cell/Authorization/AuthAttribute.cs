using Microsoft.AspNetCore.Mvc.Filters;

namespace Placement_Management_Cell.Authorization
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            if (httpContext.Session.GetString("role") == null)
            {
                httpContext.Response.Redirect("/Student/Login");
                return;
            }
        }
    }
}
