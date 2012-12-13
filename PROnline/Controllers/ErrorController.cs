using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;

namespace PROnline.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/


        public ActionResult Error(String message,String title)
        {
            ViewBag.Error = message;
            ViewBag.Head = title;
            return View();
        }

        public ActionResult Index()
        {
            var model = new Error();
            var httpException = RouteData.Values["httpException"] as HttpException;
            var httpCode = (httpException == null) ? 500 : httpException.GetHttpCode();

            switch (httpCode)
            {
                case 403:
                    Response.StatusCode = 403;
                    model.Heading = "禁止访问！";
                    model.ErroCode = "403";
                    model.Message = "You aren't authorised to access this page.";
                    break;
                case 404:
                    Response.StatusCode = 404;
                    model.ErroCode = "404";
                    model.Heading = "页面不存在！";
                    model.Message = "We couldn't find the page you requested.";
                    return Content("");
                    break;
                case 500:

                    Response.StatusCode = 500;
                    model.ErroCode = "500";
                    model.Heading = "内部错误！";
                    model.Message = httpException.Message;
                    break;
                default:
                    break;
            }

            Response.TrySkipIisCustomErrors = true;
            return View(model);
        }


    }
}
