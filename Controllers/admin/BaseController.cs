using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MJRPAdmin.Controllers.admin
{
    public class BaseController : Controller
    {
        List<string> authenitcateAction { get; set; }
        List<string> authorizeAction { get; set; } = new List<string>();


        public void authenticate(string actions)
        {
            string[] actionsArr = actions.Split(",");
            this.authenitcateAction = actionsArr.ToList();
        }

        public void authorize(string actions)
        {
            string[] actionsArr = actions.Split(",");
            this.authorizeAction = actionsArr.ToList();
            authorizeAction = authorizeAction.ConvertAll(d => d.ToLower());
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var httpContextAccessor = new HttpContextAccessor();
            string loggedInUserEmail = httpContextAccessor.HttpContext.Session.GetString("Email");
            int? loggedInUserType = httpContextAccessor.HttpContext.Session.GetInt32("UserType");
            //var controller = httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();
            string currentAction = httpContextAccessor.HttpContext.Request.RouteValues["action"].ToString();
            currentAction = currentAction.ToLower();

            if (this.authorizeAction.Contains(currentAction) && string.IsNullOrEmpty(loggedInUserEmail))
            {
                Response.Redirect("../../Auth/Login");
            }
            else if (this.authorizeAction.Contains(currentAction) && !string.IsNullOrEmpty(loggedInUserEmail))
            {
                if (loggedInUserType != 1)
                    Response.Redirect("../../Auth/Login");
            }
        }
    }
}
