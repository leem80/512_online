using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Users;

namespace PROnline.Src
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public String MenuId { get; set; }
        private bool isOk = false;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (MenuId != null)
            {
                if (filterContext.HttpContext.Session["role"] == null)
                {
                    filterContext.HttpContext.Session["role"] = "public";
                }
                string role = (String)filterContext.HttpContext.Session["role"];
                if (MenuId != null)
                {
                    base.OnActionExecuting(filterContext);
                    HttpSessionStateBase session = filterContext.HttpContext.Session;
                    List<String> limit = PROnline.Src.Utils.getMenuLimit(session);
                    if (limit.Contains(MenuId) || role == "admin")
                    {
                        isOk = true;
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (MenuId != null && !isOk)
            {
                filterContext.Result = new RedirectResult("/Account/Login?from="+filterContext.HttpContext.Request.Url);
            }


        }
    }
}